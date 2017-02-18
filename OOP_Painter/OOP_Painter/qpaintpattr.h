#ifndef QPAINTPATTR_H
#define QPAINTPATTR_H

#include <QBrush>;
#include <QPen>;
#include <QAbstractGraphicsShapeItem>;


class QPaintPattr
{
public:
    QPaintPattr();
    void SetBrushColor(QBrush BrushColor);
    void SetPenColor(QPen PenColor);
    void SaveToFile(QString FileName);
    void

private:
    QBrush BrushColor;
    QPen PenColor;
    QList<QAbstractGraphicsShapeItem> ItemsList;

};

#endif // QPAINTPATTR_H
