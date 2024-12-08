$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Opportunities/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Opportunities/EditModal');

    var dataTable = $('#OpportunitiesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.opportunities.opportunity.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Opportunities.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Opportunities.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('OpportunityDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.opportunities.opportunity
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('EstimatedCloseDate'),
                    data: "estimatedCloseDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
                },
                {
                    title: l('Stage'),
                    data: "stage",
                },
                {
                    title: l('Icon'),
                    data: "icon",
                },
                {
                    title: l('Notes'),
                    data: "notes",
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewOpportunityButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
