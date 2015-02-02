using System;

namespace WebParsingFramework
{
    /// <summary>
    /// ��� ��������-���.
    /// </summary>
    [Flags]
    public enum WebPriceType
    {
        /// <summary>
        /// ��������������
        /// </summary>
        None = 0,

        /// <summary>
        /// ������-����.
        /// </summary>
        Internet = 1,

        /// <summary>
        /// ��������� ����.
        /// </summary>
        Retail = 2,

        /// <summary>
        /// ��� ����.
        /// </summary>
        All = Internet | Retail
    }
}