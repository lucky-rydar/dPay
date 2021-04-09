import QtQuick 2.12
import QtQuick.Window 2.12
import QtQuick.Controls.Styles 1.4
import QtQuick.Controls.Universal 2.12
import QtQuick.Controls 2.12

Item {
    id: rootMenu

    SwipeView{
        id: rootSwipeView
        anchors.fill: parent
        currentIndex: 1
        //interactive: false //TODO: uncomment on release

        Register{
            id: regPage
            //anchors.fill: parent
        }

        Login{
            id: logPage
            //anchors.fill: parent
        }

        MainMenu{
            id: mainPage
            //anchors.fill: parent
        }
    }
}
/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
