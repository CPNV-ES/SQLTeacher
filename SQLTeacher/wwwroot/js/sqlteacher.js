$('.isActiveCheckBox').change(sender => {
    // Get the current value
    let currentValue = sender.currentTarget.checked
    // Get checkboxes list
    const checkBoxes = $('.isActiveCheckBox')
    // Put all checkboxes value to false
    for (const checkBox of checkBoxes) {
        checkBox.checked = false
    }

    // Set the current value to the clicked checkbox
    sender.currentTarget.checked = currentValue

    // Get the id of the current checkbox
    const currentId = sender.currentTarget.parentElement.parentElement.getElementsByClassName('colId')[0].innerText

    // Get the csrf token
    const antiForgeryToken = $("input[name=__RequestVerificationToken]").val()

    // Generate json
    const jsonData = JSON.stringify({ Id: currentId, IsActive: currentValue, __RequestVerificationToken: antiForgeryToken })

    // Update in db
    $.ajax({
        url: `/exercises/activate/${currentId}`,
        type: 'POST',
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) { },
        error: function (response) {
            console.log(response)
        }
    })
})