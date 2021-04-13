import QtQuick 2.12
import QtQuick.Window 2.12
import QtQuick.Controls.Styles 1.4
import QtQuick.Controls.Universal 2.12
import QtQuick.Controls 2.12
import api.wrapper 2.0

Item {
    id: rootMenu

    ApiWrapper{
        id: api
    }

    SwipeView{
        id: rootSwipeView
        anchors.fill: parent
        currentIndex: 1
        //interactive: false //TODO: uncomment on release

        Register{
            id: regPage
        }

        Login{
            id: logPage
        }

        MainMenu{
            id: mainPage
        }
    }
}
/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
