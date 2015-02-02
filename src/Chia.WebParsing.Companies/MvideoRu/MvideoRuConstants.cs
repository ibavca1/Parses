using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public class MvideoRuConstants
    {
        public const int Id = 38;

        public const string Name = "Mvideo.Ru";

        public const string SiteUri = "http://www.mvideo.ru";

        public const string SiteUriMask = "http://{0}.mvideo.ru";

        public const bool AvailabilityInShops = true;

        public const bool ProductArticle = true;

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(3);

        public static readonly WebCity[] Cities = MvideoRuCity.All.ToArray();

        public static string prefix = "";

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("���� �������","���������� � ������������� �����������","���������� ��� GPS �����������"),
new WebSiteMapPath("���� �������","���������� � ������������� �����������","����� ��� �����������"),
new WebSiteMapPath("����� �������","����������"),
new WebSiteMapPath("����� �������","������������ ��� �������","���������� ��� DJ ������������"),
new WebSiteMapPath("������������ �������","������������ ������� �������","���������� ��� ������������ �������"),
new WebSiteMapPath("���� � �����������","Blu-ray / DVD �����"),
new WebSiteMapPath("���� � �����������","Blu-ray / DVD �����","Blu-ray �����"),
new WebSiteMapPath("���� � �����������","Blu-ray / DVD �����","DVD �����"),
new WebSiteMapPath("���� � �����������","���� ��� ������� ���������"),
new WebSiteMapPath("���� � �����������","���� ��� ������� ���������","���� ��� Nintendo"),
new WebSiteMapPath("���� � �����������","���� ��� ������� ���������","���� ��� Sony Playstation"),
new WebSiteMapPath("���� � �����������","���� ��� ������� ���������","���� ��� XBOX 360"),
new WebSiteMapPath("���� � �����������","���� ��� ������� ���������","���� ��� XBOX ONE"),
new WebSiteMapPath("���� � �����������","������������ ��������� � PC ����"),
new WebSiteMapPath("���� � �����������","������������ ��������� � PC ����","����������"),
new WebSiteMapPath("���� � �����������","������������ ��������� � PC ����","������ ����������� �����������"),
new WebSiteMapPath("���� � �����������","������������ ��������� � PC ����","���� ��� ��"),
new WebSiteMapPath("���� � �����������","������������ ��������� � PC ����","��������� Microsoft Office � Windows"),
new WebSiteMapPath("���� � �����������","����������� �����������","���������� ��� ����������� ������������"),
new WebSiteMapPath("������� � ��������","������ ��� �����","������ ��� �����"),
new WebSiteMapPath("������� � ��������","������ ��� ��������","���� ����������"),
new WebSiteMapPath("������� � ��������","������ ��� ������"),
new WebSiteMapPath("��������, �������� � ����������","������������ ����������","���� ��� ����������"),
new WebSiteMapPath("���������� � �����","���������� ��� �����������","�������� �������� ��� �������"),
new WebSiteMapPath("��������","���������� ��� ���������","�������� ������ ��� ���������"),
new WebSiteMapPath("��������","���������� ��� ���������","��������� ��� ����������"),
new WebSiteMapPath("��������","�������","������-�������"),
new WebSiteMapPath("������� ��� ����","���������� ��� ����","������ ������������� ������"),
new WebSiteMapPath("������� ��� ����","������� �������","���������� �����"),
new WebSiteMapPath("������� ��� ����","������� �������","����"),
new WebSiteMapPath("������� ��� ����","���������","�����"),
new WebSiteMapPath("������� ��� ����","���������","�����������"),
new WebSiteMapPath("������� ��� ����","���������","����������� �����"),
new WebSiteMapPath("������� ��� ����","������� ������������","������ ������ ���������������"),
new WebSiteMapPath("������� ��� ����","������� ������������","�������� ������� � ������������"),
new WebSiteMapPath("������� ��� �����","���������� ��� ������� �������� �������"),
new WebSiteMapPath("������� ��� �����","���������� ��� ������� �������� �������","���������� ��� �������� ����"),
new WebSiteMapPath("������� ��� �����","���������� ��� ������� �������� �������","���������� ��� ������������ �����"),
new WebSiteMapPath("������� ��� �����","���������� ��� ������� �������� �������","���������� ��� ������������� �����"),
new WebSiteMapPath("������� ��� �����","���������� ��� ������� �������� �������","���������� ��� �������������"),
new WebSiteMapPath("������� ��� �����","���������� ��� �������� �������"),
new WebSiteMapPath("������� ��� �����","���������� ��� �������� �������","������� � ������"),
new WebSiteMapPath("������� ��� �����","���������� ��� �������� �������","�������� ��������"),
new WebSiteMapPath("������� ��� �����","������","�������� ���� � �����"),
new WebSiteMapPath("������� ��� �����","������","������ �������� ����������"),
new WebSiteMapPath("������� ��� �����","������","����� ��� ���������"),
new WebSiteMapPath("������� ��� �����","������������� ����","���������� ��� ���������"),
new WebSiteMapPath("������� ��� �����","������������� ����","����"),
new WebSiteMapPath("���� � �����","���������� ��� ���� � ������������","������������"),
new WebSiteMapPath("���� � �����","���������� ��� ���� � ������������","�������� �������� ��� ������"),
new WebSiteMapPath("���� � �����","���������� ��� ���� � ������������","���������� ��� ������������� Leica"), // ������ � ������, ������� ��������
new WebSiteMapPath("���� � �����","������������ ������� ������"), // ������ � ������, ������� ��������
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "�����",
                    "Gorenje ��������� Pininfarina",
                    "Gorenje ��������� Karim Rashid",
                    "Gorenje ��������� Classico",
                    "Siemens ��������� Aviator"
                };
    }
}