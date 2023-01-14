function OnTextAreaInput(textarea, addRowOnNewLine) {
    if(addRowOnNewLine === false)
        return;

    textarea.style.height = '0px';
    textarea.style.height = textarea.scrollHeight + 'px';
}

function SetMinMaxHeightTextArea(minRows, maxRows, id) {
    let el = document.getElementById(id);
    let lh = window.getComputedStyle(el).getPropertyValue('line-height').replace('px', '');
    let paddingBlock = parseInt(window.getComputedStyle(el).getPropertyValue('padding-block').replace('px', ''));

    if(maxRows > 1) {
        el.style.maxHeight = paddingBlock * 2 + maxRows * lh + 'px';
    }

    if(minRows > 1) {
        el.style.minHeight = paddingBlock * 2 + minRows * lh + 'px';
    }
}