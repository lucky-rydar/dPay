import QtQuick 2.0
import QtQuick.Controls 2.0

Page {
    function set_cards_model(cards){
        chooseCard.model = cards
        let to_set = []
        for(let i = 0; i < cards.length; i++){
            to_set.push(cards[i].card_token)
        }
        chooseCard.model = to_set
    }

    Item{
        id: chooseCardBox
        width: parent.width * 0.8
        height: chooseCard.height

        anchors{
            horizontalCenter: parent.horizontalCenter
            top: parent.top
            topMargin: height*3
        }

        Text{
            id: chooseCardLabel
            text: "Card receiver: "

            font{
                pointSize: Math.min(chooseCard.height/3, chooseCard.width)
            }

            anchors{
                verticalCenter: parent.verticalCenter
            }
        }

        ComboBox{
            id: chooseCard
            width: parent.width/2
            anchors{
                //horizontalCenter: parent.horizontalCenter
                top: parent.top
                topMargin: 0
                right: parent.right
            }
        }
    }

    TextField {
        id: titleField
        width: parent.width * 0.8
        placeholderText: "title"

        anchors{
            horizontalCenter: parent.horizontalCenter
            top: chooseCardBox.bottom
            topMargin: height/2
        }
    }

    TextField {
        id: descriptionField
        width: parent.width * 0.8
        placeholderText: "why you need money?"

        anchors{
            horizontalCenter: parent.horizontalCenter
            top: titleField.bottom
            topMargin: height/2
        }
    }

    Button{
        id: createDonationBtn
        text: "Create"

        anchors{
            horizontalCenter: parent.horizontalCenter
            top: descriptionField.bottom
            topMargin: height/2
        }

        onClicked: {
            if(titleField.text == "" || descriptionField.text == ""){
                creatingStatus.text = "some of fields are empty"
            }
            else{
                creatingStatus.text = ""
                let res = api.create_donation(clientUserData.token, chooseCard.currentText, titleField.text, descriptionField.text)
                console.log(res)

                titleField.clear()
                descriptionField.clear()

            }
        }
    }

    Text{
        id: creatingStatus

        text: ""

        anchors{
            horizontalCenter: parent.horizontalCenter
            top: createDonationBtn.bottom
            topMargin: height/2
        }
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.75;height:480;width:640}
}
##^##*/
