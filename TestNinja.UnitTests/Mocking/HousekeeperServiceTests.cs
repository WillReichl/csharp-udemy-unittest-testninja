using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private HousekeeperService _service;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime (2017, 1, 1);
        private readonly string _statementFileName = "filename";

        [SetUp]
        public void SetUp ()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _service = new HousekeeperService(
                _unitOfWork.Object, 
                _statementGenerator.Object, 
                _emailSender.Object, 
                _xtraMessageBox.Object);
            
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)); 
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HousekeepeEmailNotProvided_DoNotGenerateStatement(string housekeeperEmail)
        {
            _housekeeper.Email = housekeeperEmail;
            _service.SendStatementEmails(_statementDate);
            // Ensure SaveStatement method is never called
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never); 
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsNullOrWhitespace_DoNotEmailStatement(string statementFileName)
        {

            _statementGenerator.Setup(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
                es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    statementFileName,
                    It.IsAny<string>()
                ), Times.Never());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement ()
        {
            _statementGenerator.Setup(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
                es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()
                ));
        }
    }
}
