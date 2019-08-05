using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;

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
        public void DoCopyESM(Guid RecordId)

        {

            var opportunityManager = _userConnection.EntitySchemaManager.GetInstanceByName("UsrSale");
            var nameCopy = opportunityManager.CreateEntity(_userConnection);
            var copedName = opportunityManager.CreateEntity(_userConnection);
            bool exist = nameCopy.FetchFromDB(new Dictionary<string, object>()
            {
                { "Id", RecordId }
            });
            if (exist)
            {
                copedName.SetDefColumnValues();
                copedName.SetColumnValue("UsrName", nameCopy.GetColumnValue("UsrName"));
                copedName.SetColumnValue("UsrNotes", nameCopy.GetColumnValue("UsrNotes"));
                copedName.SetColumnValue("UsrLookup1Id", nameCopy.GetColumnValue("UsrLookup1Id"));
                copedName.Save();
                var detailData = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "UsrGenre");
                detailData.AddColumn("UsrOrder.Id");
                detailData.AddColumn("UsrCirculation");
                var esqFunction = detailData.CreateFilterWithParameters(FilterComparisonType.Equal,
                   "UsrOrder.Id",
                   RecordId);
                detailData.Filters.Add(esqFunction);
                var entitydetailDataColumn = detailData.GetEntityCollection(_userConnection);
                var opportunityManager1 = _userConnection.EntitySchemaManager.GetInstanceByName("UsrGenre");//созд
                foreach (var entity in entitydetailDataColumn)

                {

                    var detailsDataSet = opportunityManager1.CreateEntity(_userConnection);//создать строчку.
                    detailsDataSet.SetDefColumnValues();
                    detailsDataSet.SetColumnValue("UsrOrderId", copedName.GetColumnValue("Id"));
                    detailsDataSet.SetColumnValue("UsrCirculation", entity.GetColumnValue("UsrCirculation"));
                    detailsDataSet.Save();

                }

            }

        }
    }

}






