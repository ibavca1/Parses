using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    /// <summary>
    /// Контент веб-страницы.
    /// </summary>
    [ContractClass(typeof (WebPageContentContract))]
    public abstract class WebPageContent : IDisposable
    {
        private Encoding _encoding;
        private bool _isDisposed;
        private CookieCollection _cookies;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebPageContent"/>.
        /// </summary>
        protected WebPageContent()
            : this(Encoding.UTF8)
        {
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebPageContent"/>.
        /// </summary>
        protected WebPageContent(Encoding encoding)
        {
            _encoding = encoding;
        }

        /// <summary>
        /// Позволяет объекту очистить ресурсы и выполнить другие операции очистки перед тем, как он будет уничтожен сборщиком мусора.
        /// </summary>
        ~WebPageContent()
        {
            Dispose(false);
        }

        /// <summary>
        /// Возвращает или задает файлы cookie, связанные с этим контентом.
        /// </summary>
        public CookieCollection Cookies
        {
            get
            {
                Contract.Ensures(Contract.Result<CookieCollection>() != null);

                ThrowIfDisposed();
                return _cookies ?? (_cookies = new CookieCollection());
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Cookies");

                ThrowIfDisposed();
                _cookies = value;
            }
        }

        /// <summary>
        /// Возвращает кодировку символов, используемую для кодирования текста контента.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                Contract.Ensures(Contract.Result<Encoding>() != null);

                ThrowIfDisposed();
                return _encoding;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Encoding");

                ThrowIfDisposed();
                _encoding = value;
            }
        }

        public bool IsJavaScriptProcessed { get; set; }

        public static readonly WebPageContent Empty = new StringWebPageContent(string.Empty);

        /// <summary>
        /// Асинхронно возвращает содержимое в виде массива байтов.
        /// </summary>
        /// <param name="cancellationToken">Токен <see cref="CancellationToken"/>, который будет назначен задаче.</param>
        /// <returns>Задача, содержащая содержимое в виде массива байтов.</returns>
        public virtual Task<byte[]> ReadAsByteArrayAsync(CancellationToken cancellationToken)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            
            ThrowIfDisposed();
            return Task.Factory.StartNew(() => ReadAsByteArray(), cancellationToken);
        }

        /// <summary>
        /// Возвращает содержимое в виде массива байтов.
        /// </summary>
        /// <returns>Содержимое в виде массива байтов.</returns>
        public abstract byte[] ReadAsByteArray();

        /// <summary>
        /// Асинхронно возвращает содержимое в виде потока.
        /// </summary>
        /// <param name="cancellationToken">Токен <see cref="CancellationToken"/>, который будет назначен задаче.</param>
        /// <returns>Задача, содержащая содержимое в виде потока.</returns>
        public virtual Task<Stream> ReadAsStreamAsync(CancellationToken cancellationToken)
        {
            Contract.Ensures(Contract.Result<Stream>() != null);

            ThrowIfDisposed();
            return Task.Factory.StartNew(() => ReadAsStream(), cancellationToken);
        }

        /// <summary>
        /// Возвращает содержимое в виде потока.
        /// </summary>
        /// <returns>Содержимое в виде потока.</returns>
        public virtual Stream ReadAsStream()
        {
            Contract.Ensures(Contract.Result<Stream>() != null);

            ThrowIfDisposed();
            byte[] array = ReadAsByteArray();
            return new MemoryStream(array);
        }

        /// <summary>
        /// Асинхронно возвращает содержимое в строки.
        /// </summary>
        /// <param name="cancellationToken">Токен <see cref="CancellationToken"/>, который будет назначен задаче.</param>
        /// <returns>Задача, содержащая содержимое в виде строки.</returns>
        public virtual Task<string> ReadAsStringAsync(CancellationToken cancellationToken)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            ThrowIfDisposed();
            return Task.Factory.StartNew(() => ReadAsString(), cancellationToken);
        }

        /// <summary>
        /// Возвращает содержимое в виде строки.
        /// </summary>
        /// <returns>Содержимое в виде строки.</returns>
        public virtual string ReadAsString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            ThrowIfDisposed();
            byte[] array = ReadAsByteArray();
            return Encoding.GetString(array);
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <param name="disposing">true - если метод вызывается из Dispose(); false - если из финализатора.</param>
        protected virtual void Dispose(bool disposing)
        {
            
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }

    [ContractClassFor(typeof (WebPageContent))]
    abstract class WebPageContentContract : WebPageContent
    {
        public override byte[] ReadAsByteArray()
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            return null;
        }
    }
}