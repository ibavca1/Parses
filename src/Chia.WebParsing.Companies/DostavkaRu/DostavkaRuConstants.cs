using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DostavkaRu
{
    internal static class DostavkaRuConstants
    {
        public const int Id = 609;

        public const string Name = "DostavkaRu";

        public const string SiteUri = "http://www.dostavka.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = WebCities.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("������������������", "������������� �����������", "��������",
                    "������������� ������"),
new WebSiteMapPath("������������������", "������������� �����������", "��������",
                    "������������� ������������"),
new WebSiteMapPath("������������������", "������������� �����", "����� �����"),
new WebSiteMapPath("������������������", "������������� ����"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� �����, ���������, ������ �����"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������", "���������� ��� �������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ������������� �������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ���������, ���������, ���������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ���������, ���������, ��������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ����, �������, ���-�����"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ���������, ������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ���������� � ������������� �����"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "���������� ��� ������������� � �������������"),
new WebSiteMapPath("������� �������", "���������� ��� ������� �������",
                    "�������� ��������, ������� �����"),
new WebSiteMapPath("������� �������", "����� ������� �������",
                    "������: �����, �����, ��������������", "���������� �����"),
new WebSiteMapPath("������� �������", "����� ������� �������", "����������, ����, ���������",
                    "���������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "�������� � ����"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "���������� ����, �������, �����������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "���������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "���������� ��������������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "�������, ���������, �������, �������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "�������, ��������� ��� �������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "�����, ����������, ���������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "�������� ��������������", "��������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "������ ��� ������������� ����", "�����-������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "���������� �����", "���������� ��� ������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "���������� �����", "�������� ������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "���������� �����", "�������� �������"),
new WebSiteMapPath("������� �������", "����� ������� �������", "������ � ������� ��� ����",
                    "������� ��� ����", "������� ��� ����"),
new WebSiteMapPath("������� ������"),
new WebSiteMapPath("������������ �������", "Wi-Fi, �������� � ����", "������������ ���� (WiFi)",
                    "������� � ����������"),
new WebSiteMapPath("������������ �������", "Wi-Fi, �������� � ����", "��������� ����",
                    "����������� � �������"),
new WebSiteMapPath("������������ �������", "Wi-Fi, �������� � ����", "��������� ����",
                    "����-����� � ����� ������"),
new WebSiteMapPath("������������ �������", "Wi-Fi, �������� � ����", "��������� ����",
                    "������� � ����������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� �����������", "USB �������������, ����"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� �����������", "������ �������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� ���������", "�������� ������� � �/� ��� ���������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� ���������", "��������� ��� ���������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� ���������", "��������� �������������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� ���������", "�������� ������ ��� ���������"),
new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������",
                    "���������� ��� ���������", "������� � �������� ��� ���������"),
new WebSiteMapPath("������������ �������", "�������������", "������, ������, �����������"),
new WebSiteMapPath("������������ �������", "�������������", "����������� ��� �����������"),
new WebSiteMapPath("������������ �������", "�������������", "������ ��� ��������"),
new WebSiteMapPath("������������ �������", "�������������", "������ ��� �����������"),
new WebSiteMapPath("������������ �������", "����������", "������� �������",
                    "������������ ����� (�������)"),
new WebSiteMapPath("������������ �������", "����������� �����������", "����������"),
new WebSiteMapPath("������������ �������", "����������� �����������", "������������ �������"),
new WebSiteMapPath("������������ �������", "����������� �����������", "������� ���������"),
new WebSiteMapPath("������������ �������", "��������� ���������", "�������� �������� ��� �������"),
new WebSiteMapPath("������������ �������", "��������� ���������", "�������� �������� ��� �������",
                    "�������� �������� ��� ��������� � �����������"),
new WebSiteMapPath("������������ �������", "��������� � ������� ������������", "�������������"),
new WebSiteMapPath("������������ �������", "��������� � ������� ������������",
                    "�������, ���������, �����"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�����-, ����������� � �����������"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�����-, ����������� � �����������", "������ HDMI"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�����-, ����������� � �����������", "������ ����������"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�����-, ����������� � �����������", "����������� ��� ����� � �����"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�������� �������� ��� ��, ����� � �����"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�������� �������� ��� ��, ����� � �����",
                    "������������� �������� �������� ��� ����� � �����"),
new WebSiteMapPath("��, �����, ���� � �����", "���������� ��� ����� � �����",
                    "�������� �������� ��� ��, ����� � �����", "�������� �������� ��� �������"),
new WebSiteMapPath("��, �����, ���� � �����", "����, ��������� � ����������", "����"),
new WebSiteMapPath("��, �����, ���� � �����", "����, ��������� � ����������", "����",
                    "���� ��� PlayStation 3"),
new WebSiteMapPath("��, �����, ���� � �����", "����, ��������� � ����������", "����", "���� ��� PSP"),
new WebSiteMapPath("��, �����, ���� � �����", "����, ��������� � ����������", "����",
                    "���� ��� Xbox 360"),
new WebSiteMapPath("��, �����, ���� � �����", "��������� � ������", "���������� ��� ����������"),
new WebSiteMapPath("��, �����, ���� � �����", "���� � �����", "���������� ��� ���� � �����",
                    "�������� �������� ��� ���� � �����", "��� ���������� �������"),
new WebSiteMapPath("��, �����, ���� � �����", "���� � �����", "���������� ��� ���� � �����",
                    "�������� �������� ��� ���� � �����",
                    "������������� �������� �������� ��� ���� � �����"),
new WebSiteMapPath("�������� � �����", "���������� ��� iPhone",
                    "�������� ���������� � ������ ��� iPhone", "������ � ����������� ��� iPhone"),
new WebSiteMapPath("�������� � �����", "���������� ��� iPhone",
                    "������� � �������� ������ ��� iPhone"),
new WebSiteMapPath("�������� � �����", "���������� ��� iPhone",
                    "������� � �������� ������ ��� iPhone", "�������� ������ ��� iPhone"),
new WebSiteMapPath("�������� � �����", "���������� ��� ���������",
                    "���������, ���������, ���-������� ��� ���������", "������������� ���������"),
new WebSiteMapPath("������ ��� ����", "������� �����", "�������������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "������� �����", "�������� ��� ����� ������"),
new WebSiteMapPath("������ ��� ����", "������� �����", "�������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "������� �����", "�������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "���������� ��� ������ �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "������� ��� ������ �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "������� ��� ������ �������",
                    "������� �����������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "������������� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "����� � �������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "��������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "��������� ��� ������", "������ ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "��������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "������ ��� ���� � �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ������", "������ ��� ����� �� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ��������", "����� ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ��������", "����� ���������",
                    "������ � ����� ��� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ��������", "����� ���������", "��������� ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ��������", "����� ���������", "����-����������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ��������", "�����, �������, ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����",
                    "�������� � ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����",
                    "�������� ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����",
                    "�������� ��� ���� � �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����", "�����")
,
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����",
                    "�������, ���������, ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���������� ��� ������������� ����",
                    "�����, ������, ������������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "��� ��� ������������� ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "��� ��� ������������� ����",
                    "����� ��� ��������� � ������ ��� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������� ��� �������� ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������� ��� �������� ���������",
                    "���������� ��� �������� ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������� ��� �������� ���������", "�������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������� ��� �������� ���������", "��������")
,
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "�������� ��������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "�������� ��������",
                    "���������, ��������, ��������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "�������� ��������", "���������, �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���� � ����������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���� � ����������� �����", "������ �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���� � ����������� �����", "����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���� � ����������� �����",
                    "����������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "���� � ����������� �����",
                    "������� � ��������� ��� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������ ��� ������������� ��� � ����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������ ��� ������������� ��� � ����",
                    "���������� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������ ��� ������������� ��� � ����",
                    "����� � ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "������ ��� ������������� ��� � ����",
                    "�����-������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �����", "��������� ��� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����",
                    "��������� �������� �������� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����",
                    "��������� �������� �������� ������", "�������� � ������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����",
                    "��������� �������� �������� ������", "����� � ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� ��� ���������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� ��� ���������� �����",
                    "���������� ��� ���������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� ��� ���������� �����",
                    "�����, ��������, ����������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� ��� ���������� �����",
                    "��������, ��������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� ������� � ������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "�������� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� ���������� �����", "������ � �������� �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "������, �������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "������, �������", "������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "������, �������", "�������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "�����, ���������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "�����, ���������", "������������ �������")
,
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "�����, ���������", "�������"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "�����, ���������", "�����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "�����, ���������", "��������� � ���������")
,
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "���������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "���������� �����",
                    "������� ���������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "���������� �����",
                    "��������� ����������� �����"),
new WebSiteMapPath("������ ��� ����", "��� ��� �������", "���������� �����",
                    "��������� �������� ����������� �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "�������� ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "�������� ��������",
                    "���������� ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "�������� ��������",
                    "�������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "�������� ��������",
                    "������������ ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "�������� ��������",
                    "����������������� ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "��������-���������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "��������� ���"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "��������� ����������� ��� ������ �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "��������� ����������� ��� ������� �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "���������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "��������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "��������� ������������� �������",
                    "����������� ��� �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������������� �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������������� �����",
                    "��������� �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������������� �����",
                    "���������� �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������������� �����", "�������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "������������ ���������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "��������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "���������� ����������� ��� ������ �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "���������� ����������� ��� ������� �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "������������ �����"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "����� ����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "�������� ����������� ��� ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "���������� ������������� �������",
                    "����������� ������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "������������ ������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "�������� ������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "��������� ������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "��������� ������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "���������� ������� �����������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "����������� ��� ��������"),
new WebSiteMapPath("������ ��� ����", "������������� �������", "������� ������������� �������",
                    "�������� ������"),
new WebSiteMapPath("������ ��� ����", "��������", "������, �������, �������"),
new WebSiteMapPath("������ ��� ����", "��������", "������, �������, �������", "������������"),
new WebSiteMapPath("������ ��� ����", "��������", "���������"),
new WebSiteMapPath("������ ��� ����", "��������", "���������", "��������� ��� ���� � ����"),
new WebSiteMapPath("������ ��� ����", "������ �� �������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "���������� ����� � ����������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������", "������� �����"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������", "����� ��� ������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������", "�������� ��� ������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������", "������ � �������, �����"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "������", "�����, �����, �����"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "�������� ����� � ������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "�������� ����� � ������", "�������"),
new WebSiteMapPath("������ ��� ����", "������������� ������", "�������� � ������ ��������"),
new WebSiteMapPath("������ ��� ����", "�����, ������, �������", "��������� ����")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "�������",
                    "������� ������",
                    "������",
                    "��������� ������",
                    "��������� � �������",
                    "����",
                    "����������, ��� � ������"
                };
    }
}