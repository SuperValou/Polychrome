using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using LightLogs;
using LightLogs.API;
using LightLogs.LogsManagement;
using NUnit.Framework;

namespace LightLogsTests
{
    public class LogSystemTests
    {
        private LogSystem _logSystem;

        [SetUp]
        public void Setup()
        {
            _logSystem = new LogSystem();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_logSystem.DefaultLogFilePath))
            {
                File.Delete(_logSystem.DefaultLogFilePath);
            }

            _logSystem.Dispose();
        }

        [Test]
        public void Initialize_NoParameters_ReturnsNonNullLoggerWithLogFile()
        {
            var logger = _logSystem.Initialize();

            Assert.IsNotNull(logger);
            Assert.IsTrue(File.Exists(_logSystem.DefaultLogFilePath));
        }

        [Test]
        public void Initialize_ValidLogLevel_ReturnsNonNullLoggerWithLogFile()
        {
            var logger = _logSystem.Initialize(LogLevel.Info);
            
            Assert.IsNotNull(logger);
            Assert.IsTrue(File.Exists(_logSystem.DefaultLogFilePath));
        }

        [Test]
        public void Initialize_InvalidLogLevel_ThrowsException()
        {
            var logSystem = new LogSystem();

            Assert.Throws<InvalidEnumArgumentException>(() => logSystem.Initialize((LogLevel)int.MaxValue));
        }

        [Test]
        public void Dispose_NotInitialized_DoesNotThrowException()
        {
            var logSystem = new LogSystem();

            Assert.DoesNotThrow(() => logSystem.Dispose());
        }
    }
}