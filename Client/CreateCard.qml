import QtQuick 2.0
import QtQuick.Controls 2.0

Page {
    id: page

    GroupBox {
        id: groupBox
        width: parent.width/1.3
        height: parent.height/1.7
        anchors.verticalCenter: parent.verticalCenter
        anchors.horizontalCenter: parent.horizontalCenter

        Text{
            text: "Create card"
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.top: groupBox.top
            anchors.topMargin: height/1
            font.pointSize: 12
        }

        TextField {
            id: cardNameField
            width: parent.width
            placeholderText: "card name"
            anchors.verticalCenter: parent.verticalCenter
            anchors.verticalCenterOffset: -height*1.5
            anchors.horizontalCenter: parent.horizontalCenter
        }

        Text{
            id: currencyLabel
            text: "Currency: "
            font.pointSize: 12

            anchors{
                top: cardNameField.bottom
                topMargin: height/2
                left: cardNameField.left
                leftMargin: 0
            }
        }

        ComboBox{
            id: currency
            model: ["select currency", "USD", "EUR", "UAH", "RUB"]
            anchors{
                top: cardNameField.bottom
                topMargin: 0
                right: cardNameField.right
                rightMargin: 0
            }
        }

        Text{
            id: creatingStatus
            anchors.bottom: createBtn.top
            anchors.bottomMargin: 0
            anchors.horizontalCenter: createBtn.horizontalCenter
        }

        Button {
            id: createBtn
            text: "Create"
            anchors.bottom: parent.bottom
            anchors.bottomMargin: height/2
            anchors.horizontalCenter: parent.horizontalCenter

            onClicked: {
                if(currency.currentText == "select currency"){
                    creatingStatus.text = "Currency is not selected"
                }
                else{
                    creatingStatus.text = ""
                    let res = api.add_card(clientUserData.token, cardNameField.text, currency.currentText)
                    console.log(res);
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
