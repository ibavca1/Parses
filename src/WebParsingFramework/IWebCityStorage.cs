using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// ���������� �������.
    /// </summary>
     [ContractClass(typeof(WebCityStorageContract))]
    public interface IWebCityStorage
    {
        /// <summary>
        /// �������� ����� �� ���������� ��������������.
        /// </summary>
        /// <param name="id">���������� ������������� ������.</param>
        /// <returns>��������� ������.</returns>
        /// <exception cref="ObjectNotFoundException">����� � ������������� ��������������� �����������.</exception>
        WebCity Get(int id);

        /// <summary>
        /// �������� ������ ������ �������.
        /// </summary>
        /// <returns>��������� �������.</returns>
        IEnumerable<WebCity> GetAll();
    }

     [ContractClassFor(typeof(IWebCityStorage))]
     abstract class WebCityStorageContract : IWebCityStorage
     {
         public WebCity Get(int id)
         {
             Contract.Ensures(Contract.Result<WebCity>() != null);
             return null;
         }

         public IEnumerable<WebCity> GetAll()
         {
             Contract.Ensures(Contract.Result<IEnumerable<WebCity>>() != null);
             return null;
         }
     }
}