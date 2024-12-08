$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Sales/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Sales/EditModal');

    var dataTable = $('#SalesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.sales.sale.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Sales.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Sales.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('SaleDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.sales.sale
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
                    title: l('ProductId'),
                    data: "productId",
                },
                {
                    title: l('ServiceId'),
                    data: "serviceId",
                },
                {
                    title: l('CustomerId'),
                    data: "customerId",
                },
                {
                    title: l('Quantity'),
                    data: "quantity",
                },
                {
                    title: l('TotalAmount'),
                    data: "totalAmount",
                },
                {
                    title: l('SaleDate'),
                    data: "saleDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
                },
                {
                    title: l('ReceiptPhoto'),
                    data: "receiptPhoto",
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

    $('#NewSaleButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
