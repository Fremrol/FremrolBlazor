export async function InitializeScrollLoader(container, checker, dotNetObj, autoLoad) {
    let viewport = FindClosestScrollContainer(checker);

    if(viewport === undefined) {
        viewport = container;
    }

    await TryScrollLoad(checker, dotNetObj, viewport, true);

    while(autoLoad) {
        if(await TryScrollLoad(checker, dotNetObj, viewport, true) === false) {
            break;
        }
    }

    viewport.addEventListener("wheel", () => TryScrollLoad(checker, dotNetObj, viewport, false));
    viewport.addEventListener("scroll", () => TryScrollLoad(checker, dotNetObj, viewport, false));
}

async function TryScrollLoad(checker, dotNetObj, viewport, ignoreDelay) {
    let viewportHeight = viewport.offsetTop + viewport.clientHeight;
    let isVisible = IsObjectVisible(checker, viewportHeight);

    if(isVisible) {
        return await dotNetObj.invokeMethodAsync('LoadMore', ignoreDelay);
    }

    return false;
}

function IsObjectVisible(obj, viewportHeight) {
    let rect = obj.getBoundingClientRect();

    if(viewportHeight === undefined)
        return rect.bottom > 0 && rect.top < document.documentElement.clientHeight;

    return rect.bottom > 0 && rect.top < viewportHeight;
}

function FindClosestScrollContainer(element) {
    while (element) {
        const style = getComputedStyle(element);
        if (style.overflowY !== 'visible') {
            return element;
        }
        element = element.parentElement;
    }
    return undefined;
}