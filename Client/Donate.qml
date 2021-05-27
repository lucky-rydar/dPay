import QtQuick 2.0
import QtQuick.Controls 2.0

Page {
    function set_donation_data(title, description, donation_token){
        titleLabel.text = title
        descriptionLabel.text = description
        donationTokenLabel.text = donation_token
    }

    function set_cards_model(cards){
        chooseCard.model = cards
        let to_set = []
        for(let i = 0; i < cards.length; i++){
            to_set.push(cards[i].card_token)
        }
        chooseCard.model = to_set
    }

    GroupBox{
        id: foundDonationPayMenu
        width: parent.width
        height: parent.height - donationTokenField.height
        visible: false

        Text {
            id: titleLabel
            text: "title"
            font.pointSize: 12
            anchors{
                horizontalCenter: parent.horizontalCenter
                top: parent.top
                topMargin: height*2
            }

        }

        Text {
            id: descriptionLabel
            text: "description"
            font.pointSize: 10

            anchors{
                horizontalCenter: parent.horizontalCenter
                top: titleLabel.top
                topMargin: height*2
            }
        }

        Text {
            id: donationTokenLabel
            text: "Donation token"
            font.pointSize: 10

            anchors{
                horizontalCenter: parent.horizontalCenter
                top: descriptionLabel.top
                topMargin: height*2
            }
        }

        ComboBox{
            id: chooseCard
            width: parent.width *0.8
            anchors{
                horizontalCenter: parent.horizontalCenter
                bottom: amountField.top
                bottomMargin: height/1.6
            }
        }

        TextField{
            id: amountField
            width: parent.width *0.8

            anchors{
                horizontalCenter: parent.horizontalCenter
                bottom: sendBtn.top
                bottomMargin: height/1.6
            }
        }

        Button {
            id: sendBtn
            text: "Send"
            anchors{
                bottom: parent.bottom
                bottomMargin: height/1.5
                horizontalCenter: parent.horizontalCenter
            }

            function isFloat(n){
                return Number(n) === n && n % 1 !== 0;
            }

            onClicked: {
                let res = api.donate(clientUserData.token, chooseCard.currentText, donationTokenLabel.text, amountField.text)
                console.log(res)
                amountField.text = ""
            }
        }
    }

    TextField{
        id: donationTokenField
        placeholderText: "donation token"
        width: parent.width*0.8
        anchors{
            bottom: parent.bottom

        }
    }

    Button{
        id: findDonateBtn
        text: "Find"
        width: parent.width*0.2

        anchors{
            left: donationTokenField.right
            bottom: parent.bottom
        }

        onClicked: {
            let res = api.donation_by_token(donationTokenField.text);
            let parsed = JSON.parse(res);

            if(parsed.exists){
                set_donation_data(parsed.title, parsed.description, donationTokenField.text);

                foundDonationPayMenu.visible = true
            }
        }
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;height:480;width:640}
}
##^##*/
