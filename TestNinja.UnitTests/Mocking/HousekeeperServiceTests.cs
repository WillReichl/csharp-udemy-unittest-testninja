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

        [SetUp]
        public void SetUp ()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" }
            }.AsQueryable());

            var service = new HousekeeperService(
                _unitOfWork.Object, 
                _statementGenerator.Object, 
                _emailSender.Object, 
                _xtraMessageBox.Object);
            var statementDate = new DateTime (2017, 1, 1);

            service.SendStatementEmails(statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(
                1, // oid
                "b", // name
                statementDate)); // statement date
        }
    }
}
