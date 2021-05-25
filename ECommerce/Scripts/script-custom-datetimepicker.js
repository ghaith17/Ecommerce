$('#DateId').daterangepicker(
    {
        "singleDatePicker": true,
        "startDate": moment(),
        "endDate": moment().endOf('year'),
        "opens": "center"
    });