using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Sobey.TimeLine.Model;

namespace Sobey.TimeLine.Service
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<NewsModel> GetDataBefore(DateTime time);

        [OperationContract]
        List<NewsModel> GetDataAfter(DateTime time);

    }

}
