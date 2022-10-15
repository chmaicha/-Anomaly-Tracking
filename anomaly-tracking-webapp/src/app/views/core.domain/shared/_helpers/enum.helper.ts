export interface IEnumEntry<T> {

    key: string;
    label: string;
    value: number | string;
    selected: boolean;
    data: T;
}

export class EnumHelper {

    public static getEnumValues<T>(translatePrefix: string, enumType: any, entity: { new(): T; } = null, type: string = "number"): IEnumEntry<T>[] {
        var entries: IEnumEntry<T>[] = [];

        const keys = Object.keys(enumType)
            .filter(key => typeof enumType[key as any] === type)
            .map(key => entries.push(<IEnumEntry<T>>{
                key: key,
                value: enumType[key],
                label: `${translatePrefix}${key.toLowerCase()}`,
                selected: false,
                data: entity ? new entity() : {}
            }));

        return entries;
    }
}
