using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RbtRu
{
    internal static class RbtRuConstants
    {
        public const int Id = 65;

        public const string Name = "RBT.Ru";

        public const string SiteUri = "http://www.rbt.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly RbtRuCity[] SupportedCities = RbtRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("������������� �����������","�����������"),
new WebSiteMapPath("������������� �����������","�������������"),
new WebSiteMapPath("������������� �����������","��������� ����������� ���������"),
new WebSiteMapPath("������������� �����������","������������ �������������"),
new WebSiteMapPath("������������� �����������","����� � �������"),
new WebSiteMapPath("�������� �����","���������� ��� ���������"),
new WebSiteMapPath("�������� �����","���������� ��� �������� ������"),
new WebSiteMapPath("�������� �����","��������"),
new WebSiteMapPath("�������� �����","������� � ������ ��� �������"),
new WebSiteMapPath("�������� �����","������� � �������"),
new WebSiteMapPath("�������� �����","������ ��� ��������"),
new WebSiteMapPath("�������� �����","�������� ������"),
new WebSiteMapPath("�������� �����","������� � ������ ��� ������"),
new WebSiteMapPath("�������� �����","������ ��� �������"),
new WebSiteMapPath("�������� �����","��������"),
new WebSiteMapPath("������������ �������","���������� ��� ������� ������"),
new WebSiteMapPath("������������ �������","���������� ��� ������������"),
new WebSiteMapPath("������������ �������","������������ ������� �������"),
new WebSiteMapPath("����������� ��� ����"),
new WebSiteMapPath("������������� �������","���������� ��� ������������� �������"),
new WebSiteMapPath("���������� � ����������","����� ��� ������� ������"),
new WebSiteMapPath("���������� � ����������","������������ ���������"),
new WebSiteMapPath("���������� � ����������","����������� � ����������"),
new WebSiteMapPath("���������� � ����������","������"),
new WebSiteMapPath("���������� � ����������","���������"),
new WebSiteMapPath("���������� � ����������","���� ��� ����������"),
new WebSiteMapPath("���������� � ����������","����� ������������"),
new WebSiteMapPath("���������� � ����������","�������� ��������"),
new WebSiteMapPath("�������� �������","������������ ������"),
new WebSiteMapPath("�������� �������","���������� ��� ���������"),
new WebSiteMapPath("�������� �������","���������� ��� ����"),
new WebSiteMapPath("�������� �������","���������� � ������ ��� ���"),
new WebSiteMapPath("�������� �������","���������� � ������ ��� ��������"),
new WebSiteMapPath("�������� �������","���������� � �������������"),
new WebSiteMapPath("�������� �������","�������� ��������������"),
new WebSiteMapPath("�������� �������","������ �������� ��������"),
new WebSiteMapPath("�������� �������","����"),
new WebSiteMapPath("�������� �������","������ ��� ��������"),
new WebSiteMapPath("�������� �������","������������� �����"),
new WebSiteMapPath("�������� �������","����������"),
new WebSiteMapPath("�������� �������","������� ��� ���������"),
new WebSiteMapPath("�������� �������","�������� ���������"),
new WebSiteMapPath("����� � �����������","���� ��� ������� ���������"),
new WebSiteMapPath("������� ������������� �����","���������� ��� �����"),
new WebSiteMapPath("������� ������������� �����","������� ��� ������ �����"),
new WebSiteMapPath("������� �������"),
new WebSiteMapPath("����-�����-�����","����� ��� �������"),
new WebSiteMapPath("������� ��� ����","����������  ��� ������� �����"),
new WebSiteMapPath("������� ��� ����","���������� ��� ���������"),
new WebSiteMapPath("������� ��� ����","���������� ��� ������ � �������� ������"),
new WebSiteMapPath("������� ��� ����","���������� ��� ������"),
new WebSiteMapPath("������� ��� ����","������������ ������� ������"),
new WebSiteMapPath("������� ��� ����","�������� �������� �������"),
new WebSiteMapPath("������� ��� ����","������� ��� ������"),
new WebSiteMapPath("������� ��� ����","��������"),
new WebSiteMapPath("������� ��� ����","�����������"),
new WebSiteMapPath("������� ��� ����","������� ��� �����"),
new WebSiteMapPath("������� ��� ����","������ ��� ������"),
new WebSiteMapPath("������ ��� �����"),
new WebSiteMapPath("�������� ����������","������ ��� ���������"),

                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "���������� �����"
                };
    }
}