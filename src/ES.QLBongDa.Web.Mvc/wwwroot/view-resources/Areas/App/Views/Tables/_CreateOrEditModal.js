(function ($) {
    app.modals.CreateOrEditTableModal = function () {

        var _tablesService = abp.services.app.tables;

        var _modalManager;
        var _$tableInformationForm = null;

		        var _TableclubLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Tables/ClubLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Tables/_TableClubLookupTableModal.js',
            modalClass: 'ClubLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$tableInformationForm = _modalManager.getModal().find('form[name=TableInformationsForm]');
            _$tableInformationForm.validate();
        };

		          $('#OpenClubLookupTableButton').click(function () {

            var table = _$tableInformationForm.serializeFormToObject();

            _TableclubLookupTableModal.open({ id: table.maclb, displayName: table.clubTENCLB }, function (data) {
                _$tableInformationForm.find('input[name=clubTENCLB]').val(data.displayName); 
                _$tableInformationForm.find('input[name=maclb]').val(data.id); 
            });
        });
		
		$('#ClearClubTENCLBButton').click(function () {
                _$tableInformationForm.find('input[name=clubTENCLB]').val(''); 
                _$tableInformationForm.find('input[name=maclb]').val(''); 
        });
		


        this.save = function () {
            if (!_$tableInformationForm.valid()) {
                return;
            }
            if ($('#Table_maclb').prop('required') && $('#Table_maclb').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }

            

            var table = _$tableInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _tablesService.createOrEdit(
				table
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditTableModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);