var pinCodeValid = false

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

$('.submitQuery').click(sender => {
    // Get the query of the sender
    const query = sender.currentTarget.parentElement.parentElement.getElementsByClassName('queryResponse')[0].value
    // Get the id of the sender
    const currentId = sender.currentTarget.parentElement.parentElement.getElementsByClassName('colId')[0].innerText
    // Get the user pin code
    const pinCode = $('#pinCode')[0].value

})

$('#pinCode').keyup(sender => {
    const currentPinCode = sender.currentTarget.value
    pinCodeValid = (currentPinCode.length == 4)

    for (let button of $('.submitQuery').toArray()) {
        console.log(pinCodeValid)
        input = button.parentElement.parentElement.getElementsByClassName('queryResponse')[0]
        button.disabled = !(pinCodeValid && input.value.trim() != "")
    }
})

$('.queryResponse').keyup(sender => {
    const button = sender.currentTarget.parentElement.parentElement.getElementsByClassName('submitQuery')[0]
    button.disabled = !(sender.currentTarget.value.trim() != "" && pinCodeValid)
})
