
describe('1st tests', () => {
    it('true is true', () => expect(true).toBe(true));

    it('2nd test fails', () => {
        expect(0).toBe(1);
    });
});

describe('sub test', () => {
    it('3rd test always fails', () => {
        expect(0).toBe(1);
    });

    it('true is true', () => expect(true).toBe(true));
});