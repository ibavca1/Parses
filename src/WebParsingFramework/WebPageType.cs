namespace WebParsingFramework
{
    /// <summary>
    /// ��� ���-��������.
    /// </summary>
    public enum WebPageType
    {
        /// <summary>
        /// ����������
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// �������.
        /// </summary>
        Main = 1,

        /// <summary>
        /// �������� ������.
        /// </summary>
        Product = 2,

        /// <summary>
        /// ������ �������.
        /// </summary>
        Catalog = 3,

        /// <summary>
        /// ������.
        /// </summary>
        Razdel = 4,

        /// <summary>
        /// ������ ��������.
        /// </summary>
        RazdelsList = 5,

        /// <summary>
        /// �������� ���������� ��������.
        /// </summary>
        Shop = 6,

        /// <summary>
        /// ������ ��������� ���������
        /// </summary>
        ShopsList = 7
    }
}