import QtQuick 2.0
import QtQuick.Controls 2.12

Page {
    id: page

    TextField{
        id: usernameField
        anchors.verticalCenter: parent.verticalCenter
        anchors.verticalCenterOffset: -1*passwordField.height * 0.8
        anchors.horizontalCenter: parent.horizontalCenter
        placeholderText: "username"

    }
    TextField{
        id: passwordField
        placeholderText: "password"
        anchors.top: usernameField.bottom
        anchors.horizontalCenter: parent.horizontalCenter
        anchors.topMargin: 0
        echoMode: "Password"
    }

    Button{
        id: loginBtn
        text: "Login"
        anchors.top: passwordField.bottom
        anchors.right: passwordField.right
        width: passwordField.width/2

        onClicked: {
            var status = api.login(usernameField.text, passwordField.text)
            console.log(status.logined)

            if(status.logined)
            {
                // so we logined
                console.log(status)
                //TODO: turn main menu and save received data

                loginStatus.text = ""
                usernameField.text = ""
                passwordField.text = ""
                loginStatus.text = ""
            }
            else
            {
                loginStatus.text = "cant login"
                passwordField.text = ""
            }
        }
    }

    Button{
        id: turnRegMenuBtn
        text: "New account"
        width: passwordField.width/2
        anchors.right: loginBtn.left
        anchors.top: loginBtn.top

        onClicked:{
            usernameField.text = ""
            passwordField.text = ""
            rootSwipeView.currentIndex = 0

        }

    }

    Text{
        id: loginStatus
        height: usernameField.height
        width: usernameField.width
        font.pointSize: turnRegMenuBtn.font.pointSize
        //text: "status" // make empty on release
        text: ""
        verticalAlignment: Text.AlignVCenter
        anchors.left: turnRegMenuBtn.left
        anchors.top: turnRegMenuBtn.bottom
        anchors.leftMargin: 0
        anchors.topMargin: 0
    }



}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
