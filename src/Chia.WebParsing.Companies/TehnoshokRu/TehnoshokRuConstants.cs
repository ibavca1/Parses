using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnoshokRu
{
    internal static class TehnoshokRuConstants
    {
        public const int Id = 119;

        public const string Name = "Tehnoshok.Ru";

        public const string UriMask = "http://{0}.tshok.ru";

        public const string SiteUri = "http://tshok.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly TehnoshokRuCity[] SupportedCities = TehnoshokRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("����","�������������"),
new WebSiteMapPath("����","���������"),
new WebSiteMapPath("����","�������������� ���������"),
new WebSiteMapPath("����","�������������� ���������","����������������"),
new WebSiteMapPath("����","�������������� ���������","������ ��� ������������"),
new WebSiteMapPath("����","�������������� ���������","������������"),
new WebSiteMapPath("������� ������� �������"),
new WebSiteMapPath("������� ������� �������","����������"),
new WebSiteMapPath("������� ������� �������","����������","������������ ������"),
new WebSiteMapPath("������� ������� �������","����������","��� �������"),
new WebSiteMapPath("������� ������� �������","����������","��� ������� ������"),
new WebSiteMapPath("������� ������� �������","����������","��� ������������� �����"),
new WebSiteMapPath("������� ������� �������","����������","��� ���������� �����"),
new WebSiteMapPath("������� ������� �������","����������","��� �������������"),
new WebSiteMapPath("������� ������� �������","����������","��������"),
new WebSiteMapPath("������� ������� �������","����������","������� ��� �����"),
new WebSiteMapPath("�������� ��������������","�������� ����������"),
new WebSiteMapPath("�������� ��������������","�������� �����"),
new WebSiteMapPath("�������� ��������������","����� ��������"),
new WebSiteMapPath("�������� ��������������","����� ��������","���������������"),
new WebSiteMapPath("�������� ��������������","����� ��������","����������� �����"),
new WebSiteMapPath("�������� ��������������","������ �����"),
new WebSiteMapPath("�������� ��������������","����������"),
new WebSiteMapPath("�������� ��������������","�����������"),
new WebSiteMapPath("�������� ��������������","������","����������� ������"),
new WebSiteMapPath("�������� ��������������","������","������ ��� ���"),
new WebSiteMapPath("�������� ��������������","������","�����"),
new WebSiteMapPath("�������� ��������������","������","�����"),
new WebSiteMapPath("�������� ��������������","������","�������"),
new WebSiteMapPath("�������� ��������������","������ ������������"),
new WebSiteMapPath("�������� ��������������","���������"),
new WebSiteMapPath("�������� ��������������","Ҹ���"),
new WebSiteMapPath("�������� ��������������","�����-������"),
new WebSiteMapPath("�������� ��������������","��������"),
new WebSiteMapPath("����� ������� �������","����������"),
new WebSiteMapPath("����� ������� �������","����������","� �������"),
new WebSiteMapPath("����� ������� �������","����������","� ������������������"),
new WebSiteMapPath("����� ������� �������","����������","� ���������� ������"),
new WebSiteMapPath("����� ������� �������","����������","� ����������"),
new WebSiteMapPath("����� ������� �������","����������","� ���. ��������� � ��������������"),
new WebSiteMapPath("����� ������� �������","����������","� ������������� �����"),
new WebSiteMapPath("����� ������� �������","����������","� ������������"),
new WebSiteMapPath("����� ������� �������","����������","� ����������"),
new WebSiteMapPath("����� ������� �������","����������","� ���������"),
new WebSiteMapPath("����� ������� �������","����������","� ���������","������ ��������"),
new WebSiteMapPath("����� ������� �������","����������","� ���������","������� ��� ��������"),
new WebSiteMapPath("����� ������� �������","����������","� ���������","������� ��� ��������"),
new WebSiteMapPath("����� ������� �������","����������","� ������"),
new WebSiteMapPath("����� ������� �������","����������","� ������. ���. ������"),
new WebSiteMapPath("����� ������� �������","����������","� ����������"),
new WebSiteMapPath("����� ������� �������","����������","���������� ���������"),
new WebSiteMapPath("����� ������� �������","����������","����������� ��������"),
new WebSiteMapPath("����� ������� �������","����������","������� ��� �����"),
new WebSiteMapPath("����� ������� �������","����������","������������"),
new WebSiteMapPath("����� ������� �������","������� � ��������","������������� �������","������� �������������"),
new WebSiteMapPath("����� ������� �������","������� ��� ����","���������� �����"),
new WebSiteMapPath("����� ������� �������","������� ��� ����","������"),
new WebSiteMapPath("����� ������� �������","������� ��� ����","������","������"),
new WebSiteMapPath("����� ������� �������","������� ��� ����","�����������"),
new WebSiteMapPath("�������� � ��","���������� � ��������� ���������","��� ���������","����� ��� ���������"),
new WebSiteMapPath("�������� � ��","���������� � ��������� ���������","��� ���������","���������"),
new WebSiteMapPath("�������� � ��","���������� � ��������� ���������","��� ���������","�������� ��������"),
new WebSiteMapPath("�������� � ��","���������� � ��������� ���������","������� ��� �����"),
new WebSiteMapPath("�������� � ��","���������� � ��������� ���������","����������� �����������"),
new WebSiteMapPath("����������, �����, �����","����������","��������������"),
new WebSiteMapPath("����������, �����, �����","����������","�������� ������"),
new WebSiteMapPath("��������","����������","�������� �����"),
new WebSiteMapPath("��������","����������","������������� �������"),
new WebSiteMapPath("���� � �����������","����������","�����������")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "������ ��� ������, ���� � �������",
                    "����",
                    "����",
                    "������������ ����������"
                };
    }
}