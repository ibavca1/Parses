using System;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// �����.
    /// </summary>
    public class WebCity
    {
        private readonly int _id;
        private readonly string _name;

        /// <summary>
        /// ������� ����� ��������� ������ <see cref="WebCity"/> � ��������������� � ���������.
        /// </summary>
        /// <param name="id">�������������.</param>
        /// <param name="name">��������.</param>
        /// <exception cref="ArgumentException">������������� ����� 0.</exception>
        /// <exception cref="ArgumentNullException">�������� ����� null.</exception>
        /// <exception cref="ArgumentException">�������� ������ ��� ������� ������ �� ��������.</exception>
        public WebCity(int id, string name)
        {
            Contract.Requires<ArgumentException>(id != 0, "id");
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name), "name");

            _id = id;
            _name = name;
        }

        /// <summary>
        /// ������� ����� ��������� ������ <see cref="WebCity"/> �� ������� ���������� � ��������.
        /// </summary>
        /// <param name="city">��������� ������.</param>
        /// <param name="name">��������.</param>
        /// <exception cref="ArgumentNullException">��������� ������ ����� null.</exception>
        /// <exception cref="ArgumentNullException">�������� ����� null.</exception>
        /// <exception cref="ArgumentException">�������� ������ ��� ������� ������ �� ��������.</exception>
        protected WebCity(WebCity city, string name)
        {
            Contract.Requires<ArgumentNullException>(city != null, "city");
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name), "name");

            _id = city.Id;
            _name = name;
        }

        /// <summary>
        /// ���������� �������������.
        /// </summary>
        [Pure]
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// ��������.
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
        /// ���������� ������ <see cref="string"/>, ������� ������������ ������� ������ <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// ������ <see cref="String"/>, �������������� ������� ������ <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// ����������, ����� �� �������� ������ <see cref="WebCity"/> �������� ������� <see cref="WebCity"/>.
        /// </summary>
        /// <returns>
        /// �������� true, ���� �������� ������ <see cref="WebCity"/> ����� �������� ������� <see cref="WebCity"/>; � ��������� ������ � �������� false.
        /// </returns>
        /// <param name="other">������� <see cref="WebCity"/>, ������� ��������� �������� � ������� ��������� <see cref="WebCity"/>. </param>
        [Pure]
        public bool Equals(WebCity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id != default(int) && other.Id == Id;
        }

        /// <summary>
        /// ����������, ����� �� �������� ������ <see cref="Object"/> �������� ������� <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// �������� true, ���� �������� ������ <see cref="Object"/> ����� �������� ������� <see cref="Object"/>; � ��������� ������ � �������� false.
        /// </returns>
        /// <param name="obj">������� <see cref="Object"/>, ������� ��������� �������� � ������� ��������� <see cref="Object"/>. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(WebCity)) return false;
            return Equals((WebCity)obj);
        }

        /// <summary>
        /// ������ ���� ���-������� ��� ������������� ����. 
        /// </summary>
        /// <returns>
        /// ���-��� ��� �������� ������� <see cref="object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}