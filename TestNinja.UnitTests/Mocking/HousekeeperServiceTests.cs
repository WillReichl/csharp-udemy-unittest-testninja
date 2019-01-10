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

        [SetUp]
        public void SetUp ()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                _unitOfWork.Object, 
                _statementGenerator.Object, 
                _emailSender.Object, 
                _xtraMessageBox.Object);
            
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(
                _housekeeper.Oid, // oid
                _housekeeper.FullName, // name
                _statementDate)); // statement date
        }
    }
}
