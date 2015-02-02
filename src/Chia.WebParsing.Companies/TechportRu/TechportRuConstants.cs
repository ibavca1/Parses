using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TechportRu
{
    internal static class TechportRuConstants
    {
        public const int Id = 614;

        public const string Name = "Techport.Ru";

        public const string SiteUri = "http://www.techport.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly TechportRuCity[] SupportedCities = TechportRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Hi-Fi � ������������","������������ ��� �������","DJ �����������"),
new WebSiteMapPath("Hi-Fi � ������������","������������ ��� �������","DJ �������, ������"),
new WebSiteMapPath("Hi-Fi � ������������","������������ ��� �������","���������� ��� DJ ������������"),
new WebSiteMapPath("����-����","�������� � �����","���������� �����"),
new WebSiteMapPath("����-����","���������� ��� ����","���������������"),
new WebSiteMapPath("����-����","���������� ��� ����","���������� ��� ����������"),
new WebSiteMapPath("����-����","���������� ��� ����","��������� ��� �����������"),
new WebSiteMapPath("����-����","���������� ��� ����","� ������ ��������"),
new WebSiteMapPath("����-����","���������� ��� ����","��������� ������"),
new WebSiteMapPath("����-����","���������� ��� ����","��������� �����"),
new WebSiteMapPath("����-����","���������� ��� ����","�������� ��� ������"),
new WebSiteMapPath("����-����","��� ��� ����������","���������"),
new WebSiteMapPath("����-����","��� ��� ����������","���� �� �����������","�������������"),
new WebSiteMapPath("����-����","��� ��� ����������","���� �� �����������","������������� �����"),
new WebSiteMapPath("����-����","��� ��� ����������","���� �� �����������","���������� ��� ��������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","�������� �������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","���� ��������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","����� � ����"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","��������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","������ ��� ATV"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","�������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","�������� ��� ATV"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","��������� �������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","����� � �����"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","������ ��� ������������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","������ �������������� ATV"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","������� ��� �����������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","�������������� ����������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","����� ��������"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","����� � ������ ��� ATV"),
new WebSiteMapPath("����-����","�����������","���������� ��� �����������","����� ��� �����������"),
new WebSiteMapPath("����-����","�����������","����������"),
new WebSiteMapPath("����-����","�����������","����������� � �������������"),
new WebSiteMapPath("����-����","�����������","���������"),
new WebSiteMapPath("����-����","�����������","������� � ������"),
new WebSiteMapPath("����-����","�����������","���������"),
new WebSiteMapPath("����-����","��������������� ����"),
new WebSiteMapPath("����-����","����������"),
new WebSiteMapPath("������������� ������������","������������","����������"),
new WebSiteMapPath("������������ � ����������","���������� ��� ������������ �������","������� ��� ����"),
new WebSiteMapPath("������������ � ����������","���������� ��� ������������ �������","�������� ��������"),
new WebSiteMapPath("������������ � ����������","��������� �������������� �������","����� ��� �������"),
new WebSiteMapPath("������������ � ����������","��������","���������� � ���������","����� � ��������������"),
new WebSiteMapPath("������������ � ����������","���������� � ��������� ���������","������������"),
new WebSiteMapPath("������������ � ����������","���������� � ��������� ���������","������"),
new WebSiteMapPath("������������ � ����������","���������� � ��������� ���������","�������"),
new WebSiteMapPath("������������ � ����������","������� ������������","�����"),
new WebSiteMapPath("������� � ��������","��������","���������� �����"),
new WebSiteMapPath("������� � ��������","��������","������������� ��������"),
new WebSiteMapPath("������� � ��������","��������","��������� ��� ����","����� ��� ���������"),
new WebSiteMapPath("������� � ��������","��������","�������� ��� ����"),
new WebSiteMapPath("������� � ��������","��������","�������� ��� �������� � ��������"),
new WebSiteMapPath("������� � ��������","�������������� ����","���������� ��� �����"),
new WebSiteMapPath("������� � ��������","�������������� ����","���������� ��� ������ �����"),
new WebSiteMapPath("������� ������� �������","���������� ������","����������"),
new WebSiteMapPath("������ ������� �������","���������� �����"),
new WebSiteMapPath("������ ������� �������","������ �������� �������","���������� ��� ���������"),
new WebSiteMapPath("������ ������� �������","������ �������� �������","���������� ��� �������� ���������"),
new WebSiteMapPath("������ ������� �������","������ �������� �������","���������� ��� ��������"),
new WebSiteMapPath("������ ������� �������","������ �������� �������","������������ �����"),
new WebSiteMapPath("������ ������� �������","��������","����������"),
new WebSiteMapPath("������ ������� �������","������� ��� �����"),
new WebSiteMapPath("������ ������� �������","������� ��� �����","��������� � ����������","����"),
new WebSiteMapPath("������ ������� �������","������� ��� �����","������������� ����","����������"),
new WebSiteMapPath("��������� � ������������� �����","���������� ��� ����������"),
new WebSiteMapPath("��������� � ������������� �����","����� ��� ����������"),
new WebSiteMapPath("���������� � ������������","���������� ��� ����������� � �����","�������� �������� ��� �������"),
new WebSiteMapPath("���������� � ������������","��������� � ����"),
new WebSiteMapPath("���������� � ������������","����� ��� ���������"),
new WebSiteMapPath("���������","�������������� ������������ ��� ���"),
new WebSiteMapPath("���������","��������� ��������"),
new WebSiteMapPath("���������","���������� ���"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","���������� ��� �����������"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","������"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","����������"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","�����"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","������� � ������ �� ���������"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","������������"),
new WebSiteMapPath("���� � �����������","���������� � ����-����� �������","�������� ��� ����� �� ������� � LCD-���������"),
new WebSiteMapPath("������� �������","���������� ��� �����"),
new WebSiteMapPath("������� �������","���������� ��� �����","���� ��� ������� ���������"),
new WebSiteMapPath("������� �������","���������� ��� �����","���� ��� ������� ������� �����"),
new WebSiteMapPath("������� �������","���������� ��� �����","�������"),
new WebSiteMapPath("������� �������","���������� ��� �����","����� � ������� ������� �������"),
new WebSiteMapPath("������� �������","���������� ��� �����","����� ��������"),
new WebSiteMapPath("������� �������","���������� ��� �����","�����"),
new WebSiteMapPath("������� �������","���������� ��� �����","�������"),
new WebSiteMapPath("������� �������","���������� ��� �����","������"),
new WebSiteMapPath("������� �������","���������� ��� �����","�������"),
new WebSiteMapPath("������� �������","���������� ��� �����","����� � �����"),
new WebSiteMapPath("������� �������","���������� ��� �����","�������"),
new WebSiteMapPath("������� �������","����������� ��������"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "������������� � ������",
                    "���������� ������",
                    "����, ��� � ����",
                    "������� �������",
                    "����������",
                    "��� ��� �����",
                    "�������",
                    "�������� ��������������",
                    "������",
                    "������� � ��������",
                    "������ � �������� �����",
                    "������ ��� ����",
                    "����������� �����������",
                    "��������� ������",
                    "���������� ��� ������� �������"
                };
    }
}