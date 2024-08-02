$(document).ready(() => {
    $("#supportedPlatformsSelect").on("change", () => {
        let optionSelected = $('option:selected').text()
        $.ajax({
            url: "/portfolio/PlatformRequiredFields",
            type: 'GET',
            dataType: "json",
            data: "platform=" + optionSelected,
            success: (response) => platformChange(response)
        })
    })

    function platformChange(requiredFields) {
        let fieldSlot = document.getElementById('apiFields');
        // Loop over all children elements
        for (let i = 0; i < fieldSlot.children.length; i++) {
            let currentChild = fieldSlot.children[i];
            let currDiv = document.getElementById(currentChild.id);

            let idMatch = (field) => field + "Field" === currentChild.id;
            let isVisible = $(currDiv).is(":visible");
            // Show/Hide Fields so only required ones are visible
            if ((requiredFields.some(idMatch) && !isVisible) || (!requiredFields.some(idMatch) && isVisible)) {
                $(currDiv).toggle();
            }
        }
    }

}) 
