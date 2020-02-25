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
        public void Initialize_ValidLoggerNameValidLogLevel_ReturnsNonNullLoggerWithLogFile()
        {
            var logger = _logSystem.Initialize("myLogger", LogLevel.Info);

            Assert.IsNotNull(logger);
            Assert.IsTrue(File.Exists(_logSystem.DefaultLogFilePath));
        }

        [Test]
        public void Initialize_ValidLoggerNameValidLogLevelValidTargets_ReturnsNonNullLoggerWithoutLogFile()
        {
            var logger = _logSystem.Initialize("myLogger", LogLevel.Info, new List<ITarget>());

            Assert.IsNotNull(logger);
            Assert.AreEqual(string.Empty, _logSystem.DefaultLogFilePath);
        }

        [Test]
        public void Initialize_InvalidLogLevel_ThrowsException()
        {
            var logSystem = new LogSystem();

            Assert.Throws<InvalidEnumArgumentException>(() => logSystem.Initialize((LogLevel)int.MaxValue));
        }

        [Test]
        public void Initialize_InvalidLoggerName_ThrowsException()
        {
            var logSystem = new LogSystem();

            Assert.Throws<ArgumentException>(() => logSystem.Initialize(null, LogLevel.Warning));
            Assert.Throws<ArgumentException>(() => logSystem.Initialize(string.Empty, LogLevel.Warning));
        }

        [Test]
        public void Initialize_InvalidTargets_ThrowsException()
        {
            var logSystem = new LogSystem();

            Assert.Throws<ArgumentNullException>(() => logSystem.Initialize("myLogger", LogLevel.Warning, null));
            Assert.Throws<ArgumentException>(() =>  logSystem.Initialize("myLogger", LogLevel.Warning, new List<ITarget>() {null}));
        }

        [Test]
        public void Dispose_NotInitialized_DoesNotThrowException()
        {
            var logSystem = new LogSystem();

            Assert.DoesNotThrow(() => logSystem.Dispose());
        }
    }
}