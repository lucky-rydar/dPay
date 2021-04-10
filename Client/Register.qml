import QtQuick 2.0
import QtQuick.Controls 2.0

Page {
    id: page
    TextField{
        id: usernameField
        anchors.horizontalCenter: parent.horizontalCenter
        anchors.verticalCenter: parent.verticalCenter
        anchors.verticalCenterOffset: -1*parent.height/6
        placeholderText: "username"
    }

    TextField{
        id: emailField
        placeholderText: "e-mail"
        anchors.top: usernameField.bottom
        anchors.horizontalCenter: usernameField.horizontalCenter
    }

    TextField{
        id: phoneField
        placeholderText: "phone"
        anchors.top: emailField.bottom
        anchors.horizontalCenter: usernameField.horizontalCenter
    }

    TextField{
        id: password1
        placeholderText: "password"
        anchors.top: phoneField.bottom
        anchors.horizontalCenter: usernameField.horizontalCenter
    }

    TextField{
        id: password2
        placeholderText: "password"
        anchors.top: password1.bottom
        anchors.horizontalCenter: usernameField.horizontalCenter
    }

    Button{
        id: createAccBtn
        text: "Create"
        anchors.top: password2.bottom
        anchors.horizontalCenter: password2.horizontalCenter
        width: usernameField.width
        height: password2.height
    }

    Text{
        id: registrationStatus
        height: createAccBtn.height
        width: createAccBtn.width
        font.pointSize: createAccBtn.font.pointSize
        text: "status" // make empty on release
        verticalAlignment: Text.AlignVCenter
        anchors.left: createAccBtn.left
        anchors.top: createAccBtn.bottom
        anchors.leftMargin: 0
        anchors.topMargin: 0
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
