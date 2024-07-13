import { AppEntity } from "../types/crmTypes.types";

export function flattenAppEntities(rootEntity: AppEntity): AppEntity[] {
    const result: AppEntity[] = [];

    function flatten(entity: AppEntity) {
        if (entity.Id !== undefined) {
            result.push(entity);
        }
        if (entity.Childs && entity.Childs.length > 0) {
            entity.Childs.forEach(flatten);
        }
    }

    if (rootEntity.Id !== undefined) {
        flatten(rootEntity);
    }

    return result;
}