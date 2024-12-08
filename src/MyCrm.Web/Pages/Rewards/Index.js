$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Rewards/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Rewards/EditModal');

    var dataTable = $('#RewardsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.rewards.reward.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Rewards.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Rewards.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('RewardDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.rewards.reward
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
                    title: l('Rewardpoints'),
                    data: "rewardpoints",
                },
                {
                    title: l('CreditsDollars'),
                    data: "creditsDollars",
                },
                {
                    title: l('ConversionRate'),
                    data: "conversionRate",
                },
                {
                    title: l('ExpirationDate'),
                    data: "expirationDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
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

    $('#NewRewardButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
