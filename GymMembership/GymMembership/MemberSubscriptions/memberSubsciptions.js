$(document).ready(function () {
    const apiUrl = 'https://localhost:7209/api/Member_Subscriptions';


    console.log("HELLO");

    function formatDate(dateString) {
        if (!dateString) return ''; 
        return new Date(dateString).toISOString().split('T')[0];
    }

    function displayOutput(result) {
        let tableData = '';
        $('#message').html("<h1>Subscribers List Retrieved Successfully</h1>");

        let message = `
            <table id="data-table">
                <thead>
                    <tr>
                        <th>Members' ID</th>
                        <th>Subscriptions' ID</th>
                        <th>Original Price</th>
                        <th>Discount Value</th>
                        <th>Paid Price</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Remaining Sessions</th>                       
                    </tr>
                </thead>
                <tbody id="table-body"></tbody>
            </table>`;
        $('#output').html(message);

        result.forEach(member_Subscriptions => {
            tableData += `
                <tr>
                    <td style="display: none;">${member_Subscriptions.id}</td>
                    <td>${member_Subscriptions.membersId}</td>
                    <td>${member_Subscriptions.subscriptionsId}</td>
                    <td>${member_Subscriptions.originalPrice}</td>
                    <td>${member_Subscriptions.discountValue}</td>
                    <td>${member_Subscriptions.paidPrice}</td>
                    <td>${formatDate(member_Subscriptions.startDate)}</td>
                    <td>${formatDate(member_Subscriptions.endDate)}</td>
                    <td>${member_Subscriptions.remainingSessions}</td>                    
                    <td><button onClick="deleteSubscriber(${member_Subscriptions.id})">Delete</button></td>
                </tr>`;
        });

        $('#table-body').html(tableData);
    }

    function handleApiError(error) {
        console.error('API error:', error);
        console.log(error);
        $('#output').html('Error occurred. Please check the console for details.');
    }

    function getSubscribers(){

        let filter = parseInt($('#input-filter').val());
        let lastUrl = "";
        if(isNaN(filter)){ lastUrl = "IdFilter";}
        else{lastUrl = "IdFilter?IdFilter="+ filter;}

        console.log(lastUrl);

        $.ajax({
            type: 'GET',
            url: `${apiUrl}/${lastUrl}`,
            dataType: 'json',
            success: function(data) {
                console.log("GetSubscribersSuccess");
                displayOutput(data);
                console.log(data);
            },
            error: function(error) {
                console.log("GetSuscribersError")
                handleApiError(error);
            }
        });
    }

    window.callDropDownsAPI = function(){

        let membersAPIUrl = "https://localhost:7209/api/Member_Subscriptions/GetAvailableMembers";
       

        $.ajax({
            type: 'GET',
            url: `${membersAPIUrl}`,
            dataType: 'json',
            success: function(data) {
                console.log("Available members Worked");
                let dropDownData = '<option value="0"></option>';
                data.forEach(members =>{
                    dropDownData+=`
                    <option value=${members.id}>${members.idCardNumber}</option>
                    `;
                });

                $('#membersIdCardAdd').html(dropDownData);
                console.log(data);
            },
            error: function(error) {
                console.log("GetSuscribersError")
                handleApiError(error);
            }
        });

        let subscribersAPIURL = "https://localhost:7209/api/Subscriptions/filter";

        $.ajax({
            type: 'GET',
            url: `${subscribersAPIURL}`,
            dataType: 'json',
            success: function(data) {
                console.log("Available subscriptions Worked");
                let dropDownData = '<option value="0"></option>';
                data.forEach(subscriptions =>{
                    dropDownData+=`
                    <option value=${subscriptions.id} data-price="${subscriptions.totalPrice}">${subscriptions.code}</option>
                    `;
                });

                $('#subscriptionsCodeAdd').html(dropDownData);
                console.log(data);
                
            },
            error: function(error) {
                console.log("GetSubcribersError")
                handleApiError(error);
            }

            
        });

        let discountsAPIURL = "https://localhost:7209/api/Discounts/filter";

        $.ajax({
            type: 'GET',
            url: `${discountsAPIURL}`,
            dataType: 'json',
            success: function(data) {
                console.log("Available discounts Worked");
                let dropDownData = '<option value="0">0</option>';
                data.forEach(discounts =>{

                    dropDownData+=`
                    <option value=${discounts.value}>${discounts.value}</option>
                    `;
                });

                $('#discountValueAdd').html(dropDownData);
                console.log(data);
            },
            error: function(error) {
                console.log("GetDiscountsError")
                handleApiError(error);
            }
        });

    }

    $('#subscriptionsCodeAdd').on('input', function() {
        const selectedOption = $(this).find(':selected');
        const originalPrice = parseFloat(selectedOption.data('price')) || 0;
    
        $('#originalPriceAdd').val(originalPrice.toFixed(2));  // Set the original price to 2 decimal places
        calculatePaidPrice();  // Recalculate paid price whenever the original price changes
    });

    function calculatePaidPrice() {
        let originalPrice = parseFloat($('#originalPriceAdd').val()) || 0;
        let discount = parseFloat($('#discountValueAdd').val()) || 0;

        let paidPrice = originalPrice - (originalPrice * (discount/100));
        $('#paidPriceAdd').val(paidPrice.toFixed(2)); // Set value with 2 decimal places
    }
    $('#originalPriceAdd, #discountValueAdd').on('input', calculatePaidPrice);
   
    $('#addSubscriberForm').on('submit', function(event) {
        event.preventDefault();
        const addedSubscriptionData = {
            membersId: $('#membersIdCardAdd').val(),
            subscriptionsId: parseInt($('#subscriptionsCodeAdd').val()),
            originalPrice: parseFloat($('#originalPriceAdd').val()),
            discountValue: parseFloat($('#discountValueAdd').val()),
            paidPrice: parseFloat($('#paidPriceAdd').val()),
            startDate: ($('#startDateAdd').val()) ? new Date($('#startDateAdd').val()).toISOString() : null,
            endDate: ($('#endDateAdd').val()) ? new Date($('#endDateAdd').val()).toISOString() : null,
            remainingSessions: parseInt($('#remainingSessionsAdd').val()),
        };
        $.ajax({
            url: apiUrl,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(addedSubscriptionData),
            success: function () {
                alert("Subscriber added successfully")
                window.location.replace(`memberSubscriptions.html`);
            },
            error: function (error) {
                handleApiError(error);
            }
        });
    });

    window.deleteSubscriber = function(member_SubscriptionsId) {
        console.log(member_SubscriptionsId);
        $.ajax({
            url: `${apiUrl}/${member_SubscriptionsId}`,
            method: 'DELETE',
            success: function () {
                getSubscribers();
                alert(`Subscriber with ID ${member_SubscriptionsId} deleted successfully.`);
            },
            error: function (error) {
                handleApiError(error);
            }
        });
    }

    function logMessage(){
        console.log("Test");
    }

    

    $('#getSubscribersBtn').on('click', getSubscribers);
    $('#registerSubscriberLink').on('click', callDropDownsAPI);
    callDropDownsAPI();

    // Only auto-load subscribers if on the subscribers list page
    if (window.location.pathname.endsWith('/MemberSubscriptions/memberSubscriptions.html')) {
        getSubscribers();
    }

    // $('#addMembersBtn').on('click', addMembers);
    // $('#updateMembersBtn').on('click', updateMember);
    // $('#deleteMembersBtn').on('click', deleteMember);
    //document.getElementById('getMembersBtn').addEventListener('click', console.log("Button Clicked") );
});