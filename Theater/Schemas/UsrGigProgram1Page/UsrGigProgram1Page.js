define("UsrGigProgram1Page", [], function() {
	return {
		entitySchemaName: "UsrGigProgram",
		attributes: {},
		modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{
			"UsrSchema6Detail98c5719f": {
				"schemaName": "UsrSchema6Detail",
				"entitySchemaName": "UsrPerformance",
				"filter": {
					"detailColumn": "UsrColumnConcert",
					"masterColumn": "Id"
				}
			}
		}/**SCHEMA_DETAILS*/,
			businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
			methods: {
				getParentMethod: function() {
					var method,
					superMethod = (method = this.getParentMethod.caller) && (method.$previous ||
					((method = method.$owner ? method : method.caller) &&
					method.$owner.superclass[method.$name]));
					return superMethod;
				},
				save: function() {
					var parentSave = this.getParentMethod();
					var parentArguments = arguments;
					var checkIsValid =this.get("Operation");
				
					if (checkIsValid === Terrasoft.ConfigurationEnums.CardOperation.ADD || 
					checkIsValid === Terrasoft.ConfigurationEnums.CardOperation.EDIT)
						{
						this.checkActiveProgramm(parentSave, parentArguments);
						}
						
					else {
						parentSave.apply(this, parentArguments);
						}
				},
				
				checkActiveProgramm: function(parentSave, parentArguments){
					var valueSystemSettings;
					this.Terrasoft.SysSettings.querySysSettingsItem("UsrMaxNumberActiveDailyProgramm",
					function(value) {
						this.set("UsrMaxNumberActiveDailyProgramm", value);
					valueSystemSettings = value;
												},this);
					if (this.get("UsrLkFrequency").value !== "cba12291-886e-450c-a57b-ea5f694d2fd0") {
						parentSave.apply(this, parentArguments);
					}
					else {
						var esq = Ext.create("Terrasoft.EntitySchemaQuery", {
							rootSchemaName: "UsrGigProgram"
						});
						var esqFirstFilter = esq.createColumnFilterWithParameter(Terrasoft.ComparisonType.EQUAL, 
															"UsrLkFrequency","CBA12291-886E-450C-A57B-EA5F694D2FD0");
						var esqSecondFilter = esq.createColumnFilterWithParameter(Terrasoft.ComparisonType.EQUAL,
																			"UsrActivism","1");
						esq.filters.logicalOperation = Terrasoft.LogicalOperatorType.AND;
						esq.filters.add("esqFirstFilter", esqFirstFilter);				
						esq.filters.add("esqSecondFilter", esqSecondFilter);
						esq.getEntityCollection(function(result){
					
							if (result.collection.collection.length>=valueSystemSettings){
							alert("Cвободных концертных залов мало и допускается не более " + valueSystemSettings + 
																					" программ");
							return false;
							}
							else{
							parentSave.apply(this, parentArguments);
							}
						}, this);
					}
				}
			},
		dataModels: /**SCHEMA_DATA_MODELS*/{}/**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/[
			{
				"operation": "insert",
				"name": "UsrNamebd914752-d584-4547-b70e-c8ad0654f8e4",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "ProfileContainer"
					},
					"bindTo": "UsrName"
				},
				"parentName": "ProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "STRINGada9293f-e86d-4771-849b-b04b827a566c",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 2,
						"column": 0,
						"row": 1,
						"layoutName": "ProfileContainer"
					},
					"bindTo": "UsrCommentString",
					"enabled": true,
					"contentType": 0
				},
				"parentName": "ProfileContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "UsrGigCode008b45c0-2616-4699-9320-d83738a5f311",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Header"
					},
					"bindTo": "UsrGigCode"
				},
				"parentName": "Header",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "UsrActivism32f86c6d-e4b5-4b82-b57f-279223b50e76",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0,
						"layoutName": "Header"
					},
					"bindTo": "UsrActivism"
				},
				"parentName": "Header",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "NotesAndFilesTab",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.NotesAndFilesTabCaption"
					},
					"items": [],
					"order": 0
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NotesAndFilesTabGroup3604fcd0",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.NotesAndFilesTabGroup3604fcd0GroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "NotesAndFilesTab",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NotesAndFilesTabGridLayout3ce285d3",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "NotesAndFilesTabGroup3604fcd0",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "UsrCollectived5cb6cfe-5466-4a13-9fc3-4491ee78aa0f",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "NotesAndFilesTabGridLayout3ce285d3"
					},
					"bindTo": "UsrCollective"
				},
				"parentName": "NotesAndFilesTabGridLayout3ce285d3",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "UsrLkFrequency67af9cb8-8879-4f4e-a988-5b1360a73741",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "NotesAndFilesTabGridLayout3ce285d3"
					},
					"bindTo": "UsrLkFrequency"
				},
				"parentName": "NotesAndFilesTabGridLayout3ce285d3",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "LOOKUP1dbbdbb9-e9cb-4838-99e8-c7aa06c3a305",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "NotesAndFilesTabGridLayout3ce285d3"
					},
					"bindTo": "UsrOwner",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "NotesAndFilesTabGridLayout3ce285d3",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "UsrSchema6Detail98c5719f",
				"values": {
					"itemType": 2,
					"markerValue": "added-detail"
				},
				"parentName": "NotesAndFilesTab",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "remove",
				"name": "ESNTab"
			},
			{
				"operation": "remove",
				"name": "ESNFeedContainer"
			},
			{
				"operation": "remove",
				"name": "ESNFeed"
			}
		]/**SCHEMA_DIFF*/
	};
});
