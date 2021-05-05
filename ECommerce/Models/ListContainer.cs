using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ListContainer
    {
		public List<Item> dataList { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_dataList">Data list value</param>
		public ListContainer(List<Item> _dataList)
		{
			dataList = _dataList;
		}
	}
}