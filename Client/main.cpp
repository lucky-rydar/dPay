#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include <apiwrapper/apiwrapper.h>

int main(int argc, char *argv[])
{
    QCoreApplication::setAttribute(Qt::AA_EnableHighDpiScaling);

    QGuiApplication app(argc, argv);

    qmlRegisterType<ApiWrapper>("api.wrapper", 2, 0, "ApiWrapper");

    // here declearing all additional structs to use them in qml
    qRegisterMetaType<RegisterStatus>();
    qRegisterMetaType<LoginStatus>();

    QQmlApplicationEngine engine;
    const QUrl url(QStringLiteral("qrc:/main.qml"));
    QObject::connect(&engine, &QQmlApplicationEngine::objectCreated,
                     &app, [url](QObject *obj, const QUrl &objUrl) {
        if (!obj && url == objUrl)
            QCoreApplication::exit(-1);
    }, Qt::QueuedConnection);
    engine.load(url);

    return app.exec();
}
