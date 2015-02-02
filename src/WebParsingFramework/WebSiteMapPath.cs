using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace WebParsingFramework
{
    /// <summary>
    /// Путь в карте веб-сайта.
    /// </summary>
    [Serializable]
    public class WebSiteMapPath : IComparable, IComparable<WebSiteMapPath>
    {
        private readonly string[] _elements;
        private readonly string[] _codedElements;

        static WebSiteMapPath()
        {
            Empty = new WebSiteMapPath();
        }

        /// <summary>
        /// Создает пустой путь в карте сайта.
        /// </summary>
        public WebSiteMapPath()
        {
            _elements = new string[0];
            _codedElements = new string[0];
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebSiteMapPath"/> из элементов.
        /// </summary>
        /// <param name="elements">Элементы пути в карте веб-сайта.</param>
        public WebSiteMapPath(ICollection<string> elements)
        {
            Contract.Requires<ArgumentNullException>(elements != null, "elements");
            Contract.Requires<ArgumentException>(Contract.ForAll(elements, e => !string.IsNullOrWhiteSpace(e)), "elements");

            _elements = elements.ToArray();
            _codedElements = elements.Select(MakeCodedElement).ToArray();
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebSiteMapPath"/> из элементов.
        /// </summary>
        /// <param name="elements">Элементы пути в карте веб-сайта.</param>
        public WebSiteMapPath(params string[] elements)
            : this((ICollection<string>)elements)
        {
            Contract.Requires<ArgumentNullException>(elements != null, "elements");
        }

        /// <summary>
        /// Элементы пути.
        /// </summary>
        [Pure]
        public string[] Elements
        {
            get
            {
                Contract.Ensures(Contract.Result<string[]>() != null);
                return _elements;
            }
        }

        /// <summary>
        /// Если путь пуcтой, то true, иначе false.
        /// </summary>
        [Pure]
        public bool IsEmpty
        {
            get { return !Elements.Any(); }
        }

        /// <summary>
        /// Пустой путь.
        /// </summary>
        public static WebSiteMapPath Empty { get; private set; }

        /// <summary>
        /// Добавляет новый элемент в путь карты веб-сайта, создавая новый путь.
        /// </summary>
        /// <param name="elements">Добавляемые элементы.</param>
        /// <returns>Новый путь в карте веб-сайта.</returns>
        /// <exception cref="ArgumentNullException">Добавляемый элемент пустой.</exception>
        public WebSiteMapPath Add(params string[] elements)
        {
            Contract.Requires<ArgumentNullException>(elements != null, "element");
            Contract.Requires<ArgumentException>(Contract.ForAll(elements, e => !string.IsNullOrWhiteSpace(e)), "elements");

            var parts = Elements.Union(elements).ToArray();
            return new WebSiteMapPath(parts);
        }

        /// <summary>
        /// Добавляет новый элемент в путь карты веб-сайта, создавая новый путь.
        /// </summary>
        /// <returns>Новый путь в карте веб-сайта.</returns>
        public WebSiteMapPath RemoveLastElement()
        {
            Contract.Requires(IsEmpty == false, "Can't remove last element from empty path");

            var elements = Elements.Take(Elements.Length - 1).ToArray();
            return new WebSiteMapPath(elements);
        }

        /// <summary>
        /// Проверяет, удовлетворяет ли заданных путь текущему.
        /// </summary>
        /// <param name="path">Проверяемый путь в карте сайта.</param>
        /// <returns>true - если удовлетворяет; иначе - false</returns>
        public bool IsSatisfy(WebSiteMapPath path)
        {
            Contract.Requires<ArgumentNullException>(path != null, "path");

            if (_codedElements.Length < path._codedElements.Length)
                return false;

            for (int i = 0; i < path._codedElements.Length; i++)
            {
                string element = _codedElements[i];
                string otherElement = path._codedElements[i];

                if (!string.Equals(element, otherElement))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет, содержит ли текущий путь указанный элемент.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool ContainsElement(string element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");

            string codedElement = MakeCodedElement(element);
            return _codedElements.Any(e => e.IndexOf(codedElement, StringComparison.InvariantCultureIgnoreCase) != -1);
        }

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="WebSiteMapPath"/> текущему объекту <see cref="WebSiteMapPath"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="WebSiteMapPath"/> равен текущему объекту <see cref="WebSiteMapPath"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="other">Элемент <see cref="WebSiteMapPath"/>, который требуется сравнить с текущим элементом <see cref="WebSiteMapPath"/>. </param>
        [Pure]
        public bool Equals(WebSiteMapPath other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _codedElements.SequenceEqual(other._codedElements);
        }


        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="Object"/> текущему объекту <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="Object"/> равен текущему объекту <see cref="Object"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="obj">Элемент <see cref="Object"/>, который требуется сравнить с текущим элементом <see cref="Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as WebSiteMapPath);
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа. 
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return _codedElements.Aggregate(0, (current, element) => (current * 397) ^ element.GetHashCode());
            }
        }

        /// <summary>
        /// Сравнивает текущий экземпляр с другим объектом того же типа и возвращает целое число, которое показывает, 
        /// расположен ли текущий экземпляр перед, после или на той же позиции в порядке сортировки, что и другой объект.
        /// </summary>
        /// <returns>
        /// Значение, указывающее, каков относительный порядок сравниваемых объектов.Возвращаемые значения представляют следующие результаты сравнения.
        /// Значение Описание Меньше нуля Этот экземпляр меньше параметра <paramref name="obj"/>. 
        /// Нуль Этот экземпляр и параметр <paramref name="obj"/> равны. 
        /// Больше нуля Этот экземпляр больше параметра <paramref name="obj"/>. 
        /// </returns>
        /// <param name="obj">Объект для сравнения с данным экземпляром. </param>
        /// <exception cref="ArgumentException">Тип значения параметра <paramref name="obj"/> отличается от типа данного экземпляра. </exception>
        [Pure]
        public int CompareTo(object obj)
        {
            return CompareTo((WebSiteMapPath)obj);
        }

        /// <summary>
        /// Сравнивает текущий объект с другим объектом того же типа.
        /// </summary>
        /// <returns>
        /// Значение, указывающее, каков относительный порядок сравниваемых объектов.
        /// Расшифровка возвращенных значений приведена ниже.
        /// Значение Описание Меньше нуля Значение этого объекта меньше значения параметра <paramref name="other"/>.
        /// Нуль Значение этого объекта равно значению параметра <paramref name="other"/>. 
        /// Больше нуля. Значение этого объекта больше значения параметра <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">Объект, который требуется сравнить с данным объектом.</param>
        [Pure]
        public int CompareTo(WebSiteMapPath other)
        {
            if (other == null)
                return -1;

            for (int i = 0; i < _codedElements.Length; i++)
            {
                if (i >= other._codedElements.Length)
                    return 1;

                string element = _codedElements[i];
                string otherElement = other._codedElements[i];

                int result = String.CompareOrdinal(element, otherElement);
                if (result != 0)
                    return result;
            }

            return 0;
        }

        /// <summary>
        /// Возвращает объект <see cref="String"/>, который представляет текущий объект <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="String"/>, представляющий текущий объект <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (IsEmpty)
                return string.Empty;

            var elements = Elements.ToArray();
            var builder = new StringBuilder(elements[0]);
            for (int i = 1; i < elements.Length; i++)
            {
                builder.AppendFormat(" >> {0}", elements[i]);
            }

            return builder.ToString();
        }

        private static string MakeCodedElement(string element)
        {
            char[] chars = element.Where(char.IsLetterOrDigit).ToArray();
            return new string(chars);
        }
    }
}