using System;
using System.Diagnostics.Contracts;
using WebParsingFramework.Exceptions;

namespace WebParsingFramework
{
    /// <summary>
    /// Интернет-компания.
    /// </summary>
    public abstract class WebCompany
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Название.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Веб-адрес сайта.
        /// </summary>
        public abstract Uri SiteUri { get; }

        /// <summary>
        /// Метаданные компании.
        /// </summary>
        public abstract WebCompanyMetadata Metadata { get; }

        /// <summary>
        /// Получить сайт в указанном городе.
        /// </summary>
        /// <param name="city">Требуемый город.</param>
        /// <returns>Экземпляр сайта.</returns>
        /// <exception cref="ArgumentNullException">city является null.</exception>
        /// <exception cref="UnsupporedWebCityException">Нет сайта в указанном городе.</exception>
        public abstract WebSite GetSite(WebCity city);
        
        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="WebCompany"/> текущему объекту <see cref="WebCompany"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="WebCompany"/> равен текущему объекту <see cref="WebCompany"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="other">Элемент <see cref="WebCompany"/>, который требуется сравнить с текущим элементом <see cref="WebCompany"/>. </param>
        [Pure]
        public bool Equals(WebCompany other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
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
            return Equals(obj as WebCompany);
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа. 
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Возвращает объект <see cref="String"/>, который представляет текущий объект <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="String"/>, представляющий текущий объект <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
