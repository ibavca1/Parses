using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.PultRu
{
    internal static class PultRuConstants
    {
        public const int Id = 613;

        public const string Name = "Pult.Ru";

        public const string SiteUri = "http://pult.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = PultRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("������", "������������ ����������� � �� ����� ���� HDMI"),
                    new WebSiteMapPath("������", "������� � �����������"),
                    new WebSiteMapPath("������", "�������"),
                    new WebSiteMapPath("������", "������� ������"),
                    new WebSiteMapPath("��������", "���������� ��� ���������"),
                    new WebSiteMapPath("������������� ��������� ������", "���������� ��� ��������� ��������������"),
                    new WebSiteMapPath("������������� ��������� ������", "������� ��������������"),
                    new WebSiteMapPath("������������� ��������� ������", "�������� �� ����� � ��������"),
                    new WebSiteMapPath("������������� ��������� ������", "�������"),
                    new WebSiteMapPath("������������� ��������� ������", "��������������"),
                    new WebSiteMapPath("������� ����� ���", "���������"),
                    new WebSiteMapPath("������� ����� ���", "��������� ������"),
                    new WebSiteMapPath("������� ����� ���", "������ ���������������"),
                    new WebSiteMapPath("������� ����� ���", "����������� �����"),
                    new WebSiteMapPath("��������� ������������", "����������"),
                    new WebSiteMapPath("��������� ������������", "�������� ������������"),
                    new WebSiteMapPath("���������� � ������", "������ ��� �����������"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "������",
                    "����������"
                };
    }
}