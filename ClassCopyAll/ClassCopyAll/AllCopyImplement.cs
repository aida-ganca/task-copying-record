using System;
using Terrasoft.Core;
using Terrasoft.Core.DB;

namespace ClassCopyAll
{
    public class AllCopyImplement
    {
        private UserConnection _userConnection;
        public AllCopyImplement(UserConnection uc)//констр
        {
            _userConnection = uc;
        }
        public string GetName(Guid RecordId)
        {
            var id = Guid.NewGuid();
            var query = new Select(_userConnection)
                .Column(Column.Const(id))
                .Column("UsrName")
                .Column("UsrNotes")
                .Column("UsrLookup1Id")
            .From("UsrSale")
            .Where("Id")
                .IsEqual(Column.Parameter(RecordId)) as Select;

            var insert = new InsertSelect(_userConnection)
                .Into("UsrSale")
                .Set("Id","UsrName", "UsrNotes", "UsrLookup1Id")
                .FromSelect(query);
            insert.Execute();

            //Данные детали
            var queryDetail = new Select(_userConnection)
                    .Column(Column.Const(id))
                    .Column("UsrCirculation")
                    .Column("UsrReprinting")
                .From("UsrGenre")
                .Where("UsrOrderId")
                    .IsEqual(Column.Parameter(RecordId)) as Select;

            var insert1 = new InsertSelect(_userConnection)
                .Into("UsrGenre")
                .Set("UsrOrderId", "UsrCirculation", "UsrReprinting")
                .FromSelect(queryDetail);
            insert1.Execute();

            return string.Empty;
        }
       

    }

}






