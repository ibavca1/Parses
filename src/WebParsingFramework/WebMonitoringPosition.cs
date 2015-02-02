using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using WebParsingFramework.Utils;

namespace WebParsingFramework
{
    /// <summary>
    /// Торговая позиция.
    /// </summary>
    public class WebMonitoringPosition
    {
        private string _name;
        private decimal _onlinePrice;
        private decimal _retailPrice;
        private IList<WebProductAvailabilityInShop> _availabilityInShops;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebMonitoringPosition"/>.
        /// </summary>
        public WebMonitoringPosition()
        {
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebMonitoringPosition"/>.
        /// </summary>
        /// <param name="article">Артикул.</param>
        /// <param name="name">Наименование.</param>
        /// <param name="onlinePrice">Интернет-цена.</param>
        /// <param name="retailPrice">Розничная цена.</param>
        /// <param name="uri">Ссылка.</param>
        /// <exception cref="ArgumentNullException">Наименование пустое или равно null.</exception>
        public WebMonitoringPosition(string article, string name, decimal onlinePrice, decimal retailPrice, Uri uri)
        {
            Article = article;
            Name = name;
            OnlinePrice = onlinePrice;
            RetailPrice = retailPrice;
            Uri = uri;
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebMonitoringPosition"/>.
        /// </summary>
        /// <param name="article">Артикул.</param>
        /// <param name="name">Наименование.</param>
        /// <param name="onlinePrice">Интернет-цена.</param>
        /// <param name="retailPrice">Розничная цена.</param>
        /// <param name="uri">Ссылка.</param>
        /// <param name="uri">Характеристики.</param>
        /// <exception cref="ArgumentNullException">Наименование пустое или равно null.</exception>
        public WebMonitoringPosition(string article, string name, decimal onlinePrice, decimal retailPrice, Uri uri, string _Characteristics)
        {
            Article = article;
            Name = name;
            OnlinePrice = onlinePrice;
            RetailPrice = retailPrice;
            Uri = uri;
            Characteristics = _Characteristics;
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebMonitoringPosition"/>.
        /// </summary>
        /// <param name="article">Артикул.</param>
        /// <param name="name">Наименование.</param>
        /// <param name="onlinePrice">Интернет-цена.</param>
        /// <param name="uri">Ссылка.</param>
        /// <exception cref="ArgumentNullException">Наименование пустое или равно null.</exception>
        public WebMonitoringPosition(string article, string name, decimal onlinePrice, Uri uri)
            : this(article, name, onlinePrice, 0, uri)
        {
        }

        /// <summary>
        /// Артикул.
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Признак акции.
        /// </summary>
        public bool IsAction { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Name");
                Contract.Requires<ArgumentException>(!value.IsEmptyOrWhiteSpace(), "Name");

                _name = value;
            }
        }

        /// <summary>
        /// Ссылка на веб-страницу.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Онлайн-цена.
        /// </summary>
        public decimal OnlinePrice
        {
            get { return _onlinePrice; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0, "OnlinePrice");
                _onlinePrice = value;
            }
        }

        /// <summary>
        /// Розничная цена.
        /// </summary>
        public decimal RetailPrice
        {
            get { return _retailPrice; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0, "RetailPrice");
                _retailPrice = value;
            }
        }

        /// <summary>
        /// Характеристики.
        /// </summary>
        public string Characteristics { get; set; }

        /// <summary>
        /// Наличие в магазинах.
        /// </summary>
        public IList<WebProductAvailabilityInShop> AvailabilityInShops
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<WebProductAvailabilityInShop>>() != null);
                return _availabilityInShops ?? (_availabilityInShops = new List<WebProductAvailabilityInShop>());
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "AvailabilityInShops");
                _availabilityInShops = value;
            }
        }

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="WebMonitoringPosition"/> текущему объекту <see cref="WebMonitoringPosition"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="WebMonitoringPosition"/> равен текущему объекту <see cref="WebMonitoringPosition"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="other">Элемент <see cref="WebMonitoringPosition"/>, который требуется сравнить с текущим элементом <see cref="WebMonitoringPosition"/>. </param>
        public bool Equals(WebMonitoringPosition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Uri.Equals(other.Uri) &&
                   string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(WebMonitoringPosition)) return false;
            return Equals((WebMonitoringPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Uri.GetHashCode();
                result = (result * 397) ^ Name.ToLowerInvariant().GetHashCode();
                return result;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}