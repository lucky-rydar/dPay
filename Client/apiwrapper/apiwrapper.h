#ifndef APIWRAPPER_H
#define APIWRAPPER_H

#include <QObject>
#include <QPointer>
#include <QtNetwork>
#include <QJsonObject>
#include "registerstatus.h"
#include "loginstatus.h"

class ApiWrapper : public QObject
{
    Q_OBJECT
public:
    explicit ApiWrapper(QObject *parent = nullptr);

    // user
    Q_INVOKABLE RegisterStatus registration(QString username, QString email, QString phone, QString password);
    Q_INVOKABLE LoginStatus login(QString username, QString password);

    // card

    // transaction

private:
    QString baseUrl;

signals:

};

#endif // APIWRAPPER_H
