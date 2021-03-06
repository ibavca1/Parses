using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Oo3Ru
{
    internal static class Oo3RuConstants
    {
        public const int Id = 605;

        public const string Name = "003.Ru";

        public const string SiteUri = "http://www.003.ru";

        public static readonly bool SupportsProductArticle = false;

        public static readonly bool SupportsAvailabilityInShops = false;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] Cities = Oo3RuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                   new WebSiteMapPath("����������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","����� � �����������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� ����","�������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� ����","���������� � ����������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� ����","�����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� ����� � �������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� ����� � �������","�������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","�������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","���������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","������ ��� �������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","�����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","������������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","���������� ��� �������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","���������� ��� �������","GPS ����������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","�������/�����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","�������/�����","�����/�����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","������ ��� �������","�����/��������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","�����, �����","��������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","C������ ��������","������������� ������� ��������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","�������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","�������������","������������� �������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������������","����������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������","��������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","����� �������� �������� ��� ������� ����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","����� �������� �������� � �������� ����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������� ����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������� ����","���������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","������� ����","�����������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","�������������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","��������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","��������","�������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","��������","���������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������","�������������� �������� �������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������","������������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������","��������� ��������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������","�����"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������ ��� ���� � �������","���������� �������������","���������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","���������������� ������"),
new WebSiteMapPath("�������������� ���������","��� ��� ���� � ������","������� ���������"),
new WebSiteMapPath("�������������� ���������","������� ������","���������� ��� ���������"),
new WebSiteMapPath("�������������� ���������","������� ������","������� ����� Philips Avent","��������� � �����"),
new WebSiteMapPath("�������������� ���������","������� ������","������� ����� Philips Avent","������� ������, �������� � ���������"),
new WebSiteMapPath("�������������� ���������","������� ������","������� ����� Philips Avent","������������  � ���������� ��� ����� �� ������"),
new WebSiteMapPath("�������������� ���������","������� ������","������� ����� Philips Avent","�����-������������ � ���������� ��� �����"),
new WebSiteMapPath("�������������� ���������","������� ������","������� ������"),
new WebSiteMapPath("�������������� ���������","������� ������","�������"),
new WebSiteMapPath("�������������� ���������","����������","������������ �������","����������"),
new WebSiteMapPath("�������������� ���������","����������","������ ������� �������","����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","�������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� � ��������������� ������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","�������� ����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","�������� ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","�������� ������","�������� ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","�������� ������","������ ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","����������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","����������������","���������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","������ ������������� ������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������������� ������������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������ ������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","������ ����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ��������� ��� �����������","�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������ ��c�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","��������� ������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","�����, �����, ����� ��� �����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","�����������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����� � �����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","����� �����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","���������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","������� ��� �����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","�����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","��������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","���������� ��� �������� � ���������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ����������","�������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ����������","������ ��� ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ����������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ������","���������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ������","���������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ������","���������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ������","�����-�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� ������","�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","�������� ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","�������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","���� � ������ �����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","���������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","�������� ������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� �������","�����"),
new WebSiteMapPath("�������������� ���������","������ � ���������","������","�������� � ��������� ���������","������� ��� ��������� � ���"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","����������","������ ���������� ��� ����������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","��������������","���������� ��������������"),
new WebSiteMapPath("�������������� ���������","������ � ���������","��������������","���������� ��������������","������� �������������� (220 �, 1 ����)"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "�� ��� ���� � ����",
                    "����������",
                    "����� ���",
                    "1+1=������",
                    "��������� � ������",
                    "��� ��� �����",
                    "bork"
                };
    }
}