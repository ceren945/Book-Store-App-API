using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerManager : ILoggerService
    {

        private static ILogger logger=LogManager.GetCurrentClassLogger();// NLog adlı bir loglama kütüphanesi kullanılarak bir ILogger örneği oluşturur.
                                                                         // ILogger, loglama işlemlerini gerçekleştirmek için kullanılan bir arabirimdir.
                                                                         // LogManager.GetCurrentClassLogger() metodunu kullanarak,
                                                                         // NLog tarafından sağlanan varsayılan ILogger örneğine erişim sağlanır.



        public void LogDebug(string message) => logger.Debug(message);
      

        public void LogError(string message)=>logger.Error(message);
       

        public void LogInfo(string message)=>logger.Info(message);
       

        public void LogWarning(string message)=>logger.Warn(message);
        
    }
}
