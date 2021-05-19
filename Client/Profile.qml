import QtQuick 2.0
import QtQuick.Controls 2.0
import QtQuick.Layouts 1.3

Page {
    id: page

    function set_data(username, email, phone)
    {
        profileUsername.text = username
        profileEmail.text = email
        profilePhone.text = phone
    }

    GroupBox {
        id: groupBox
        width: parent.width/1.3
        height: parent.height/1.5
        anchors.verticalCenter: parent.verticalCenter
        anchors.horizontalCenter: parent.horizontalCenter

        Text {
            id: text1
            text: "Your profile"
            font.pointSize: 12
            anchors.top: groupBox.top
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.topMargin: height
        }

        RowLayout {
            id: rowLayout
            width: parent.width/1.3
            height: parent.height/6
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.top: parent.top
            anchors.topMargin: height
            Text{
                text: "username: "
            }
            Text{
                id: profileUsername
                text: "test"
                anchors{
                    right: parent.right
                }
            }

        }

        RowLayout {
            id: rowLayout1
            width: parent.width/1.3
            height: parent.height/6
            anchors.top: rowLayout.bottom
            Text {
                text: "email: "
            }

            Text {
                id: profileEmail
                text: "test@gmail.com"
                anchors{
                    right: parent.right
                }
            }
            anchors.horizontalCenter: parent.horizontalCenter
        }

        RowLayout {
            id: rowLayout2
            width: parent.width/1.3
            height: parent.height/6
            anchors.top: rowLayout1.bottom
            Text {
                text: "phone: "
            }

            Text {
                id: profilePhone
                text: "099123456789"
                anchors.right: parent.right
            }
            anchors.horizontalCenter: parent.horizontalCenter
        }


        TextField{
            id: newPhoneField
            placeholderText: "380990000001"
            width: parent.width/1.3
            anchors.top: rowLayout2.bottom
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.topMargin: 0
        }
        Button{
            id: newPhoneBtn
            text: "new phone"
            width: parent.width/1.3
            anchors.top: newPhoneField.bottom
            anchors.topMargin: 0
            anchors.horizontalCenter: parent.horizontalCenter

            onClicked: {
                let response = api.change_phone(clientUserData.token, newPhoneField.text)
                let from_json = JSON.parse(response)
                if(from_json.changed){
                    profilePhone.text = newPhoneField.text
                    clientUserData.phone = newPhoneField.text
                }
                newPhoneField.text = ""
            }
        }
    }
}
/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:1.100000023841858;height:480;width:640}
}
##^##*/
