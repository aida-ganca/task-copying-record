namespace Terrasoft.Configuration.CountReleasesService
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.ServiceModel.Activation;
	using Terrasoft.Core;
	using Terrasoft.Web.Common;
	using Terrasoft.Core.Entities; 

	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class CountReleasesService: BaseService
	{
		
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
		public string GetCountReleasesService(string Code){
			
			var result = -1;
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "UsrPerformance");
			
			esq.AddAllSchemaColumns();
		   	var esqFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "UsrColumnConcert.UsrGigCode", Code);
			esq.Filters.Add(esqFilter);
			var entities = esq.GetEntityCollection(UserConnection);
			if (entities.Count > 0)
			{
				result = entities.Count;
			}else {
			   	var esqOne = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "UsrColumnConcert");
				esqOne.AddAllSchemaColumns();
			   	var esqFilters = esqOne.CreateFilterWithParameters(FilterComparisonType.Equal, "UsrGigCode", Code);
				esqOne.Filters.Add(esqFilters);
				var entitiesCheck = esqOne.GetEntityCollection(UserConnection);
				 if (entitiesCheck.Count > 0)
				{
				result = 0;
				}else{
				result = -1;
				}	
			}
			return result.ToString();
		}
	}
}
