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
            text: "Add card"
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.top: groupBox.top
            anchors.topMargin: height/1
            font.pointSize: 12
        }

        TextField {
            id: cardNumberField
            width: parent.width
            placeholderText: "card number"
            anchors.verticalCenter: parent.verticalCenter
            anchors.verticalCenterOffset: -height*1.5
            anchors.horizontalCenter: parent.horizontalCenter
            maximumLength: 16
        }

        TextField {
            id: monthField
            placeholderText: "month"
            width: cardNumberField.width/2
            anchors.top: cardNumberField.bottom
            anchors.topMargin: 0
            anchors.left: cardNumberField.left
            anchors.leftMargin: 0
            maximumLength: 2
        }

        TextField {
            id: yearField
            placeholderText: "year"
            width: cardNumberField.width/2
            anchors.top: cardNumberField.bottom
            anchors.topMargin: 0
            anchors.left: monthField.right
            anchors.leftMargin: 0
            maximumLength: 2
        }

        TextField {
            id: cvvField
            placeholderText: "cvv"
            width: cardNumberField.width/2
            anchors.right: yearField.right
            anchors.top: yearField.bottom
            anchors.topMargin: 0
            anchors.rightMargin: 0
            echoMode: "Password"
            maximumLength: 3
        }

        Button {
            id: button
            text: "Add"
            anchors.bottom: parent.bottom
            anchors.bottomMargin: height/2
            anchors.horizontalCenter: parent.horizontalCenter

        }

    }

}

/*##^##
Designer {
    D{i:0;autoSize:true;height:480;width:640}
}
##^##*/
