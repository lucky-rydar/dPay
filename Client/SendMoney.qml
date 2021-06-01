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

    GroupBox {
        id: groupBox
        width: parent.width/1.3
        height: parent.height/1.4
        anchors.verticalCenter: parent.verticalCenter
        anchors.horizontalCenter: parent.horizontalCenter

        Text{
            id: menuLabel
            text: "Send money"
            font.pointSize: 12
            anchors{
                horizontalCenter: parent.horizontalCenter
                top: parent.top
                topMargin: height/2
            }

        }

        ComboBox{
            id: chooseCard
            width: parent.width *0.8
            anchors{
                horizontalCenter: parent.horizontalCenter
                top: menuLabel.bottom
                topMargin: height/2
            }
        }

        TextField {
            id: toCardField
            placeholderText: "receiver card token"

            anchors{
                top: chooseCard.bottom
                topMargin: height/2
                horizontalCenter: parent.horizontalCenter
            }

            width: parent.width*0.8
        }

        TextField {
            id: amountField
            placeholderText: "amount"

            anchors{
                top: toCardField.bottom
                topMargin: height/2
                horizontalCenter: parent.horizontalCenter
            }

            width: parent.width*0.8
        }

        Button {
            id: sendBtn
            text: "Send"
            anchors{
                top: amountField.bottom
                topMargin: height/2
                horizontalCenter: parent.horizontalCenter
            }

            onClicked: {
                if(toCardField.text != "" && amountField.text != ""){
                    api.send_by_card(clientUserData.token,
                                     chooseCard.currentText,
                                     toCardField.text.toUpperCase(),
                                     amountField.text);
                    toCardField.clear()
                    amountField.clear()
                }
            }
        }

    }
}



/*##^##
Designer {
    D{i:0;autoSize:true;height:480;width:640}
}
##^##*/
