// Global variable and classes
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
    // Get the query input
    const queryInput = sender.currentTarget.parentElement.parentElement.getElementsByClassName('queryResponse')[0]
    // Get the query of the sender
    const query = queryInput.value
    // Get the id of the sender
    const currentId = sender.currentTarget.parentElement.parentElement.getElementsByClassName('colId')[0].innerText
    // Get the user pin code
    const pinCode = $('#pinCode')[0].value

    // Generate json
    const jsonData = JSON.stringify({ query: query, pinCode: pinCode })

    $.ajax({
        url: `/api/checkAnswer/${currentId}`,
        type: 'POST',
        data: jsonData,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (response) => {
            if (response) {
                // Put green shadow
                queryInput.classList.remove('inputError')
                queryInput.classList.add('inputSuccess')
            } else {
                // Put red shadow
                queryInput.classList.remove('inputSuccess')
                queryInput.classList.add('inputError')
            }
            
        },
        error: response => {
            console.log(response)
        }
    })

})

$('#pinCode').keyup(sender => {
    // Get the current pin code
    const currentPinCode = sender.currentTarget.value
    // Check if the pin code is valid
    pinCodeValid = (currentPinCode.length == 4)
    if (pinCodeValid) {
        // Generate json
        const jsonData = JSON.stringify({ pinCode: currentPinCode })
        $.ajax({
            url: `/api/checkPinCode`,
            type: 'POST',
            data: jsonData,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: (response) => {
                pinCodeValid = response
                if (response) {

                    sender.currentTarget.classList.remove('inputError')
                    sender.currentTarget.classList.add('inputSuccess')
                } else {

                    sender.currentTarget.classList.remove('inputSuccess')
                    sender.currentTarget.classList.add('inputError')
                }
            },
            error: response => {
                console.log(response)
                pinCodeValid = false
            }
        })
    } else {
        sender.currentTarget.classList.remove('inputSuccess')
        sender.currentTarget.classList.add('inputError')
    }

    // Update button state
    for (let button of $('.submitQuery').toArray()) {
        input = button.parentElement.parentElement.getElementsByClassName('queryResponse')[0]
        button.disabled = !(pinCodeValid && input.value.trim() != "")
    }
})

$('.queryResponse').keyup(sender => {
    // Remove the shadow
    sender.currentTarget.classList.remove('inputError')
    sender.currentTarget.classList.remove('inputSuccess')
    // Get the button in the same row
    const button = sender.currentTarget.parentElement.parentElement.getElementsByClassName('submitQuery')[0]
    // Update button state
    button.disabled = !(sender.currentTarget.value.trim() != "" && pinCodeValid)
})

if (document.getElementById('examInfosPage')) {
    putScoresInTable()
    window.setInterval(() => putScoresInTable(), 5000)
}

function getScores() {
    return $.ajax({
        url: `/api/scores`,
        type: 'GET'
    })
}

function putScoresInTable() {
    getScores().then(scores => {
        scores.forEach(score => {
            if (score.success) {
                getCell(score.peopleId, score.querieId)[0].style.backgroundColor = 'rgba(76, 175, 80, 0.4)'
            } else {
                getCell(score.peopleId, score.querieId)[0].style.backgroundColor = 'rgba(244 ,67, 54, 0.4)'
            }
        })
    })
}

function getCell(column, row) {
    var column = $('#people' + column).index();
    var row = $('#query' + row)
    return row.find('td').eq(column);
}

// Exam Part
$('#addQuestion').click(sender => {
    // Get template form
    let template = $('#hidden-template').html();

    // Get the questions aera
    let questionArea = $('#questionsList')

    // Add question inputs
    questionArea.append(template)
})

$('#submitButton').click(sender => {
    // Get all the question
    let questions = $('#questionsList').children('.question')

    // Get 2 field
    let title = $('#Title')[0].value
    let dbScript = $('#DbScript')[0].value
    // Create jsonDataExercises
    let jsonDataExercise = JSON.stringify({ 'title': title, 'dbScript': dbScript })

    // Create exercises
    $.ajax({
        url: `/exercises/create`,
        type: 'POST',
        data: jsonDataExercise,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Create jsonDataQuestion
            let jsonDataQuestion = new Array()

            questions.toArray().forEach((question, index) => {
                index++
                jsonDataQuestion.push({ 'statement': question.children[1].value, 'formulation': question.children[3].value, 'rank': index, 'exerciseId': response })
            })
            // Create queries
            $.ajax({
                url: `/queries/CreateFromExercise`,
                type: 'POST',
                data: JSON.stringify(jsonDataQuestion),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response) {
                        window.location.href = '/exercises'
                    }
                },
                error: function (response) {
                    console.log(response)
                }
            })
        },
        error: function (response) {
            console.log(response)
        }
    })
    
})