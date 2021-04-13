#ifndef REGISTERSTATUS_H
#define REGISTERSTATUS_H

#include <QObject>

struct RegisterStatus
{
    Q_GADGET

public:
    bool registered;

private:
    Q_PROPERTY(bool registered MEMBER registered)

};

Q_DECLARE_METATYPE(RegisterStatus)

#endif // REGISTERSTATUS_H
