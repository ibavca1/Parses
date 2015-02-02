using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace Chia.WebParsing.Parsers
{
    public class MhtmlPageContent : HtmlPageContent
    {
        private HtmlDocument _content;
        private string _location;
        private readonly object _syncLock = new object();
        private readonly string _mhtmlContent;
        
        public MhtmlPageContent(string mhtmlContent)
            : this(mhtmlContent, Encoding.UTF8)
        {
            Contract.Requires<ArgumentNullException>(mhtmlContent != null, "mhtmlContent");

            IsJavaScriptProcessed = true;
        }

        public MhtmlPageContent(string mhtmlContent, Encoding encoding)
        {
            Contract.Requires<ArgumentNullException>(encoding != null, "encoding");
            Contract.Requires<ArgumentNullException>(mhtmlContent != null, "mhtmlContent");

            _mhtmlContent = mhtmlContent;
            Encoding = encoding;
            IsJavaScriptProcessed = true;
        }

        internal override HtmlDocument Document
        {
            get
            {
                ParseIfNeed();
                return _content;
            }
        }

        public string Location
        {
            get
            {
                ParseIfNeed();
                return _location;
            }
        }

        private void ParseIfNeed()
        {
            if (_content == null)
            {
                lock (_syncLock)
                {
                    if (_content == null)
                    {
                        var parser = new MhtmlParser(_mhtmlContent);
                        string html = parser.getHTMLText(Encoding);
                        _content= new HtmlDocument();
                        _content.LoadHtml(html);
                        _location = parser.Location;
                    }
                }
            }
        }

        public class MhtmlParser
        {
            const string BOUNDARY = "boundary";
            const string CHAR_SET = "charset";
            const string CONTENT_TYPE = "Content-Type";
            const string CONTENT_TRANSFER_ENCODING = "Content-Transfer-Encoding";
            const string CONTENT_LOCATION = "Content-Location";
            const string FILE_NAME = "filename=";

            private readonly string _mhtmlString; 
            private readonly bool _decodeImageData; 
            private readonly List<string[]> _dataset;

            private string _location;

            public MhtmlParser()
            {
                _dataset = new List<string[]>(); //Init dataset
                _decodeImageData = false; //Set default for decoding images
            }

            public MhtmlParser(string mhtml)
                : this()
            {
                _mhtmlString = mhtml;
            }

            public string Location
            {
                get { return _location; }
            }

            private List<string[]> DecompressString(Encoding e)
            {
                // init Prerequisites
                string type = "";
                string encoding = "";
                string filename = "";
                StringBuilder buffer = null;

                using (var reader = new StringReader(_mhtmlString))
                {
                    String boundary = GetBoundary(reader); // Get the boundary code
                    if (boundary == null) throw new Exception("Failed to find string 'boundary'");

                    //Loop through each line in the string
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string temp = line.Trim();
                        if (temp.Contains(boundary)) //Check if this is a new section
                        {
                            if (buffer != null) //If this is a new section and the buffer is full, write to dataset
                            {
                                var data = new string[3];
                                data[0] = type;
                                data[1] = filename;
                                data[2] = WriteBufferContent(buffer, encoding, e);
                                _dataset.Add(data);
                            }
                            buffer = new StringBuilder();
                        }
                        else if (temp.StartsWith(CONTENT_TYPE))
                        {
                            type = getAttribute(temp);
                        }
                        else if (temp.StartsWith(CHAR_SET))
                        {
                            
                        }
                        else if (temp.StartsWith(CONTENT_TRANSFER_ENCODING))
                        {
                            encoding = getAttribute(temp);
                        }
                        else if (temp.StartsWith(CONTENT_LOCATION) && _location == null)
                        {
                            _location = getAttribute(temp);
                        }
                        else if (temp.StartsWith(FILE_NAME))
                        {
                            char c = '"';
                            filename = temp.Substring(temp.IndexOf(c.ToString()) + 1,
                                temp.LastIndexOf(c.ToString()) - temp.IndexOf(c.ToString()) - 1);
                        }
                        else if (temp.StartsWith("Content-ID") || temp.StartsWith("Content-Disposition") ||
                                 temp.StartsWith("name=") || temp.Length == 1)
                        {
                            //We don't need this stuff; Skip lines
                        }
                        else
                        {
                            if (buffer != null)
                            {
                                buffer.Append(line + "\n");
                            }
                        }
                    }
                }

                return _dataset;
            }

            private string WriteBufferContent(StringBuilder buffer, string transferEncoding, Encoding encoding)
            {
                string content = buffer.ToString();

                if (transferEncoding.ToLower().Equals("base64"))
                {
                    try
                    {
                        content= DecodeFromBase64(buffer.ToString());
                    }
                    catch
                    {
                        return content;
                    }
                }

                if (transferEncoding.ToLower().Equals("quoted-printable"))
                {
                    content = GetQuotedPrintableString(content, encoding);
                }

                return content.Replace("\0","");
            }

            static public string DecodeFromBase64(string encodedData)
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
                string returnValue = Encoding.ASCII.GetString(encodedDataAsBytes);
                return returnValue;
            }

            public string GetQuotedPrintableString(string input, Encoding encoding)
            {
                byte[] workingBytes = Encoding.ASCII.GetBytes(input);

                int i = 0;
                int outputPos = i;

                while (i < workingBytes.Length)
                {
                    byte currentByte = workingBytes[i];
                    char[] peekAhead = new char[2];
                    switch (currentByte)
                    {
                        case (byte)'=':
                            bool canPeekAhead = (i < workingBytes.Length - 2);

                            if (!canPeekAhead)
                            {
                                workingBytes[outputPos] = workingBytes[i];
                                ++outputPos;
                                ++i;
                                break;
                            }

                            int skipNewLineCount = 0;
                            for (int j = 0; j < 2; ++j)
                            {
                                char c = (char)workingBytes[i + j + 1];
                                if ('\r' == c || '\n' == c)
                                {
                                    ++skipNewLineCount;
                                }
                            }

                            if (skipNewLineCount > 0)
                            {
                                // If we have a lone equals followed by newline chars, then this is an artificial
                                // line break that should be skipped past.
                                i += 1 + skipNewLineCount;
                            }
                            else
                            {
                                try
                                {
                                    peekAhead[0] = (char)workingBytes[i + 1];
                                    peekAhead[1] = (char)workingBytes[i + 2];

                                    byte decodedByte = Convert.ToByte(new string(peekAhead, 0, 2), 16);
                                    workingBytes[outputPos] = decodedByte;

                                    ++outputPos;
                                    i += 3;
                                }
                                catch (Exception)
                                {
                                    // could not parse the peek-ahead chars as a hex number... so gobble the un-encoded '='
                                    i += 1;
                                }
                            }
                            break;

                        case (byte)'?':
                            if (workingBytes[i + 1] == (byte)'=')
                            {
                                i += 2;
                            }
                            else
                            {
                                workingBytes[outputPos] = workingBytes[i];
                                ++outputPos;
                                ++i;
                            }
                            break;

                        default:
                            workingBytes[outputPos] = workingBytes[i];
                            ++outputPos;
                            ++i;
                            break;
                    }
                }

                string output = string.Empty;

                int numBytes = outputPos - 0;
                if (numBytes > 0)
                {
                    output = encoding.GetString(workingBytes, 0, numBytes);
                }

                return output;
            }
            /*
             * Finds boundary used to break code into multiple parts
             */
            private string GetBoundary(StringReader reader)
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    //If the line starts with BOUNDARY, lets grab everything in quotes and return it
                    if (line.StartsWith(BOUNDARY))
                    {
                        const string c = "\"";
                        return line.Substring(line.IndexOf(c, StringComparison.Ordinal) + 1, line.LastIndexOf(c, StringComparison.Ordinal) - line.IndexOf(c, StringComparison.Ordinal) - 1);
                    }
                }
                return null;
            }

            /*
             * split a line on ": "
             */
            private string getAttribute(String line)
            {
                const string str = ": ";
                return line.Substring(line.IndexOf(str, StringComparison.Ordinal) + str.Length, line.Length - (line.IndexOf(str, StringComparison.Ordinal) + str.Length)).Replace(";", "");
            }

            /*
             * Get an html page from the mhtml. Embeds images as base64 data
             */
            public string getHTMLText(Encoding encoding)
            {
                if (_decodeImageData) throw new Exception("Turn off image decoding for valid html output.");
                List<string[]> data = DecompressString(encoding);
                string body = "";
                //First, lets write all non-images to mail body
                //Then go back and add images in 
                for (int i = 0; i < 2; i++)
                {
                    foreach (string[] strArray in data)
                    {
                        if (i == 0)
                        {
                            if (strArray[0].Equals("text/html"))
                            {
                                body += strArray[2];
                            }
                        }
                        else if (i == 1)
                        {
                            if (strArray[0].Contains("image"))
                            {
                                body = body.Replace("cid:" + strArray[1], "data:" + strArray[0] + ";base64," + strArray[2]);
                            }
                        }
                    }
                }
                return body;
            }
        }
    }
}