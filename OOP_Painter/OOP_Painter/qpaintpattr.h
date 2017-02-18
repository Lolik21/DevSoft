#ifndef QPAINTPATTR_H
#define QPAINTPATTR_H

#include <QBrush>
#include <QPen>
#include <QGraphicsItemGroup>


class QPaintPattr
{
public:
    QPaintPattr();
    void SetBrushColor(QBrush BrushColor);
    void SetPenColor(QPen PenColor);
    void SaveToFile(QString FileName);
    void AddItem(QGraphicsItem *item);

private:
    QBrush BrushColor;
    QPen PenColor;
    QList<QGraphicsItem*> ItemsList;

};

#endif // QPAINTPATTR_H
