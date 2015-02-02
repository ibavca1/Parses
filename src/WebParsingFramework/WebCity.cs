using System;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// Город.
    /// </summary>
    public class WebCity
    {
        private readonly int _id;
        private readonly string _name;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebCity"/> с идентификатором и названием.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название.</param>
        /// <exception cref="ArgumentException">Идентификатор равен 0.</exception>
        /// <exception cref="ArgumentNullException">Название равно null.</exception>
        /// <exception cref="ArgumentException">Название пустое или состоит только из пробелов.</exception>
        public WebCity(int id, string name)
        {
            Contract.Requires<ArgumentException>(id != 0, "id");
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name), "name");

            _id = id;
            _name = name;
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebCity"/> из другого экземпляра и названия.
        /// </summary>
        /// <param name="city">Экземпляр города.</param>
        /// <param name="name">Название.</param>
        /// <exception cref="ArgumentNullException">Экземпляр города равен null.</exception>
        /// <exception cref="ArgumentNullException">Название равно null.</exception>
        /// <exception cref="ArgumentException">Название пустое или состоит только из пробелов.</exception>
        protected WebCity(WebCity city, string name)
        {
            Contract.Requires<ArgumentNullException>(city != null, "city");
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name), "name");

            _id = city.Id;
            _name = name;
        }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Pure]
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Название.
        /// </summary>
        [Pure]
        public string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return _name;
            }
        }

        /// <summary>
        /// Возвращает объект <see cref="string"/>, который представляет текущий объект <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="String"/>, представляющий текущий объект <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="WebCity"/> текущему объекту <see cref="WebCity"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="WebCity"/> равен текущему объекту <see cref="WebCity"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="other">Элемент <see cref="WebCity"/>, который требуется сравнить с текущим элементом <see cref="WebCity"/>. </param>
        [Pure]
        public bool Equals(WebCity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id != default(int) && other.Id == Id;
        }

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="Object"/> текущему объекту <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="Object"/> равен текущему объекту <see cref="Object"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="obj">Элемент <see cref="Object"/>, который требуется сравнить с текущим элементом <see cref="Object"/>. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(WebCity)) return false;
            return Equals((WebCity)obj);
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа. 
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}